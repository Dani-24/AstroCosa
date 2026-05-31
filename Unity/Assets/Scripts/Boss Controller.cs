using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : Character
{
    [Header("Stats")]
    public int HP = 100;
    int currentHP;

    public float moveSpeed;

    public int EXP = 10000;
    public float bulletSpeed = 3;

    public BossPhases actualPhase = BossPhases.Initial;
    private List<Func<IEnumerator>> attacksAvailable;

    float timeUntilNextAtk = 5;
    float timeUntilNextWanderBullet = 2;
    float wanderBulletsCont;

    [Header("Rings")]
    public Transform outerRing;
    public Transform middleRing;
    public Transform innerRing;

    [Header("Rings Radius")]
    public float outerRadius;
    public float middleRadius;
    public float innerRadius;

    [Header("Rings Ratial Speed")]
    public float outerSpeed;
    public float middleSpeed;
    public float innerSpeed;

    float actOutSpd, actMidSpd, actInnSpd;

    [Header("Rings Cannons")]
    public Transform[] outerCannons;
    public Transform[] middleCannons;
    public Transform[] innerCannons;

    [Header("Animations")]
    [SerializeField] float spawnDuration;
    bool isInvulnerable = false;
    bool isAbsorving = false;
    int absorved = 0;

    bool wandering = false;
    [SerializeField] float wanderAmpl;

    private Tween wanderTween;

    [SerializeField] bool isAttacking = false;

    [Header("Enemies")]
    public EnemyScriptableObject kamikazes;
    public EnemyScriptableObject[] otherEnemies;
    List<EnemyScriptableObject> enemiesAvailable = new();

    SquadController bossSquad;
    SquadController bossSquadInScene;

    [Header("UI")]
    [SerializeField] Slider slider_hp;
    [SerializeField] CanvasGroup hp_panel;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject circularBulletPrefab;

    bool firstAppearance = true;

    CircleCollider2D coll;
    [SerializeField] GameObject helix;

    void Start()
    {
        coll = GetComponent<CircleCollider2D>();

        isInvulnerable = true;
        currentHP = HP;

        ResetPosition(spawnDuration);

        attacksAvailable = new List<Func<IEnumerator>>
        {
            SpiralAttack
        };

        enemiesAvailable.Add(kamikazes);

        GameObject bossSquadGO = new();
        bossSquadGO.AddComponent<SquadController>();
        bossSquadGO.GetComponent<SquadController>().squadIsEnabled = false;
        //bossSquadGO.GetComponent<SquadController>().wandering = true;
        bossSquadGO.transform.parent = null;
        bossSquadGO.transform.position = Vector3.zero;
        bossSquadInScene = bossSquadGO.GetComponent<SquadController>();

        slider_hp.maxValue = HP;
        slider_hp.value = 0;
    }

    void Update()
    {
        // Radius Rotations
        if (outerRing != null) outerRing.Rotate(0f, 0f, actOutSpd * Time.deltaTime);
        if (middleRing != null) middleRing.Rotate(0f, 0f, actMidSpd * Time.deltaTime);
        if (innerRing != null) innerRing.Rotate(0f, 0f, actInnSpd * Time.deltaTime);

        if (!isAttacking && wandering)
        {
            if (timeUntilNextAtk > 0)
            {
                timeUntilNextAtk -= Time.deltaTime;

                // Waits for new attacks in Wander movement while shooting random bullets
                if (wanderBulletsCont > 0)
                    wanderBulletsCont -= Time.deltaTime;
                else
                {
                    wanderBulletsCont = timeUntilNextWanderBullet + UnityEngine.Random.Range(-1, 1);
                    GameObject bullet = Instantiate(bulletPrefab, transform);
                    bullet.GetComponent<Bullet>().speed = -bulletSpeed;
                    bullet.transform.parent = null;
                    bullet.layer = 6;
                }
            }
            else
            {
                timeUntilNextAtk = 5 + UnityEngine.Random.Range(-1, 4);
                isAttacking = true;
                wanderTween?.Kill();
                transform.DOKill();

                StartCoroutine(attacksAvailable[UnityEngine.Random.Range(0, attacksAvailable.Count)]());
            }
        }


        if (!isInvulnerable && GetCurrentHP_Percent() <= 50 && actualPhase == BossPhases.Initial)
        {
            actualPhase = BossPhases.Half;

            // Looses the first ring, the collider is shrinked
            Destroy(outerRing.gameObject);
            coll.radius = middleRadius;
            //StopAllCoroutines();

            moveSpeed *= 1.3f;

            // Other enemies can also appear when a special attack ends
            foreach (EnemyScriptableObject e in otherEnemies)
                enemiesAvailable.Add(e);

            attacksAvailable.Add(RandomBulshAttack);
        }
        if (!isInvulnerable && GetCurrentHP_Percent() <= 25 && (actualPhase == BossPhases.Initial || actualPhase == BossPhases.Half))
        {
            actualPhase = BossPhases.Final;

            // Looses the second ring, the collider is shrinked
            Destroy(middleRing.gameObject);
            coll.radius = innerRadius;
            //StopAllCoroutines();

            actInnSpd = innerSpeed *= 4;

            moveSpeed *= 1.2f;

            attacksAvailable.Add(KamikazeAttack);
            attacksAvailable.Add(VoidAttack);
        }
    }

    void ResetPosition(float duration)
    {
        actOutSpd = outerSpeed;
        actInnSpd = innerSpeed;
        actMidSpd = middleSpeed;

        transform.DOMove(new Vector3(0f, 3f, transform.position.z), duration)
            .SetEase(Ease.OutQuad)
            .OnComplete(StartWander);
    }

    void StartWander()
    {
        if (firstAppearance)
        {
            firstAppearance = false;
            slider_hp.DOValue(currentHP, 5);
            hp_panel.DOFade(1, 2);
        }

        wanderTween = DOVirtual.Float(0f, Mathf.PI * 2f, 1f / moveSpeed, t =>
        {
            float x = Mathf.Sin(t) * wanderAmpl;
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        })
        .SetLoops(-1, LoopType.Restart)
        .SetEase(Ease.Linear);

        wandering = true;
        isAttacking = false;
        isInvulnerable = false;
    }

    IEnumerator EnemySummoning()
    {
        int enemiesToSpawn = UnityEngine.Random.Range(1, 5 + (int)(0.1f * GameManagerScript.Instance.difficulty));

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject enemy = Instantiate(GameManagerScript.Instance.enemyPrefab, transform);
            enemy.transform.parent = null;
            enemy.GetComponent<EnemyController>().originalPosInSquad = new Vector2(UnityEngine.Random.Range(-2.5f, 2.5f), UnityEngine.Random.Range(-1, 2));
            enemy.GetComponent<EnemyController>().enemyData = enemiesAvailable[UnityEngine.Random.Range(0, enemiesAvailable.Count)];
            enemy.GetComponent<EnemyController>().squad = bossSquadInScene;
            enemy.GetComponent<EnemyController>().ReturnToOrigin();
            bossSquadInScene.enemies.Add(enemy);

            yield return new WaitForSeconds(0.33f);
        }
    }

    IEnumerator SpiralAttack()
    {
        // starts rotating and shoots in a spiral. The spiral cadence between each shoot increases with difficulty

        actOutSpd *= 2;
        actMidSpd *= 3;
        actInnSpd *= 5;

        for (int i = 0; i < 20 + 0.1f * GameManagerScript.Instance.difficulty; i++)
        {
            Transform[] cannons = outerCannons;

            switch (actualPhase)
            {
                case BossPhases.Half:
                    cannons = middleCannons;
                    break;
                case BossPhases.Final:
                    cannons = innerCannons;
                    break;
                default:
                    break;
            }


            foreach (var cannon in cannons)
            {
                GameObject newBullet = Instantiate(bulletPrefab, cannon.transform.position, cannon.transform.rotation);
                newBullet.GetComponent<Bullet>().speed = -bulletSpeed;
                newBullet.transform.parent = null;
                //newBullet.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                newBullet.layer = 6;
            }

            yield return new WaitForSeconds(0.4f);
        }

        yield return new WaitForSeconds(1);

        ResetPosition(4);
        // Once a special attack ends, it summons a random ammount of enemies. First only summons kamikazes
        StartCoroutine(EnemySummoning());
    }

    IEnumerator RandomBulshAttack()
    {
        // Shoots 20 bullets with rng towards the player (all the bullets have a delay between each other but all of them target the same initial position).
        // After a delay, this attack is repeated x times according to the difficulty

        for (int i = 0; i < 3 + 0.01f * GameManagerScript.Instance.difficulty; i++)
        {
            Vector3 playerPos = GameManagerScript.Instance.playerInstance.transform.position;

            for (int j = 0; j < 20 + 0.1f * GameManagerScript.Instance.difficulty; j++)
            {
                Vector2 dir = (playerPos - transform.position).normalized;
                float spread = UnityEngine.Random.Range(-7f, 7f);

                dir = Quaternion.Euler(0f, 0f, spread) * dir;

                GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.FromToRotation(Vector3.down, dir));
                newBullet.GetComponent<Bullet>().speed = -bulletSpeed;
                newBullet.transform.parent = null;
                newBullet.layer = 6;
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(1);
        }
        ResetPosition(2);
        StartCoroutine(EnemySummoning());
    }

    IEnumerator KamikazeAttack()
    {
        // Can Perform the kamikaze attack (first show some visual feedback)
        actOutSpd = Mathf.Lerp(actOutSpd, actOutSpd * 5, 2);
        actMidSpd *= Mathf.Lerp(actMidSpd, actMidSpd * 8, 2);
        actInnSpd *= Mathf.Lerp(actInnSpd, actInnSpd * 10, 2);

        yield return new WaitForSeconds(2f);

        transform.DOMove(new Vector3(transform.position.x, -6f, transform.position.z), 4f)
            .SetEase(Ease.OutQuad);

        yield return new WaitForSeconds(4.1f);

        transform.DOMove(new Vector3(transform.position.x, 3, transform.position.z), 4f)
            .SetEase(Ease.OutQuad);

        yield return new WaitForSeconds(4.1f);

        ResetPosition(2);
        StartCoroutine(EnemySummoning());
    }

    IEnumerator VoidAttack()
    {
        // Stays in place, opens the void and absorbs all damage received which heals it. Once the attack ends, it shoots a slow void bullet that
        // becames bigger depending on how many attacks blocked during the void

        isInvulnerable = isAbsorving = true;
        helix.SetActive(true);

        float attackDuration = 8;
        float elapsed = 0f;

        while (elapsed < attackDuration)
        {
            elapsed += Time.deltaTime;

            helix.transform.Rotate(0, 0, -200 * Time.deltaTime);

            yield return null;
        }

        GameObject bullet = Instantiate(circularBulletPrefab, transform);
        bullet.GetComponent<Bullet>().speed = -bulletSpeed * 1.25f;
        bullet.transform.parent = null;
        bullet.layer = 6;

        bullet.transform.localScale = Vector3.one * (0.01f * GameManagerScript.Instance.difficulty + absorved * 0.1f);

        isInvulnerable = isAbsorving = false;
        helix.SetActive(false);
        absorved = 0;

        bullet.GetComponent<Bullet>().speed = -bulletSpeed * 1.25f;
        bullet.GetComponent<Collider2D>().enabled = true;

        yield return new WaitForSeconds(2);

        ResetPosition(4);
        StartCoroutine(EnemySummoning());
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }

    public int GetCurrentHP_Percent()
    {
        return (int)((float)currentHP / HP * 100);
    }

    public override void OnReceiveDmg()
    {
        if (isAbsorving)
        {
            absorved++;
            currentHP++;
            slider_hp.value = currentHP;
        }
        if (isInvulnerable) return;

        currentHP--;
        slider_hp.value = currentHP;

        if (currentHP <= 0)
            OnDeath();
    }

    public override void OnDeath()
    {
        GameManagerScript.Instance.AddScore(EXP);
        GameManagerScript.Instance.EndGame();
        Destroy(gameObject);
    }

    public enum BossPhases
    {
        Initial,
        Half,
        Final
    }
}
