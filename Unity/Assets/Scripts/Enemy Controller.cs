using DG.Tweening;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public SquadController squad;
    public Vector2 originalPosInSquad;
    public EnemyScriptableObject enemyData;

    [SerializeField] float shootingCooldown = 10f;
    float shootingBaseCooldown;
    float shootingCont = 0;

    [SerializeField] GameObject bulletPrefab;

    bool enemyWandering = true;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enemyData.sprite;

        // Temporal DEBUG Color
        switch (enemyData.type)
        {
            case EnemyType.Kamikaze:
                spriteRenderer.color = Color.blue;
                break;
            case EnemyType.ZigZag:
                spriteRenderer.color = Color.yellow;
                break;
            case EnemyType.Hunter:
                spriteRenderer.color = Color.red;
                break;
        }

        shootingBaseCooldown = shootingCooldown;
        CalculateNextShootCd();
    }

    void Update()
    {
        Attacking();
    }

    void CalculateNextShootCd()
    {
        float baseCd = Random.Range(0, shootingBaseCooldown);
        shootingCooldown = (baseCd / (1 + GameManagerScript.Instance.difficulty * 0.05f)) * Random.Range(0.9f, 1.1f);
    }

    void Attacking()
    {
        if (shootingCont >= shootingCooldown)
        {
            shootingCont = 0;
            CalculateNextShootCd();

            int rng = Random.Range(0, 5);

            if (rng == 0 && squad.wandering && enemyWandering)
            {
                // SPECIAL ATTACK

                transform.parent = null;
                enemyWandering = false;
                float startPosX = transform.position.x;

                switch (enemyData.type)
                {
                    case EnemyType.Kamikaze:
                        transform.DOMove(new Vector3(transform.position.x, -8f, transform.position.z), enemyData.moveDuration)
                            .SetEase(Ease.Linear)
                            .OnComplete(ReturnToOrigin);
                        break;
                    case EnemyType.ZigZag:
                        transform.DOMove(new Vector3(transform.position.x, -8f, transform.position.z), enemyData.moveDuration)
                            .SetEase(Ease.Linear)
                            .OnComplete(ReturnToOrigin);

                        DOVirtual.Float(0f, Mathf.PI * 2f, 1f / enemyData.specialMoveSpeed, t =>
                        {
                            float x = Mathf.Sin(t) * enemyData.specialMoveAmpl;
                            transform.position = new Vector3(startPosX + x, transform.position.y, transform.position.z);
                        })
                        .SetLoops(-1, LoopType.Restart)
                        .SetEase(Ease.Linear)
                        .SetTarget(transform);
                        break;
                    case EnemyType.Hunter:

                        if (GameManagerScript.Instance.playerInstance == null)
                        {
                            ReturnToOrigin();
                            break;
                        }

                        float targetPosX = GameManagerScript.Instance.playerInstance.transform.position.x;
                        float targetPosY = GameManagerScript.Instance.playerInstance.transform.position.y;

                        transform.DOMove(new Vector3(targetPosX, targetPosY, transform.position.z), enemyData.moveDuration)
                            .SetEase(Ease.Linear)
                            .OnComplete(ReturnToOrigin);
                        break;
                }
            }
            else
            {
                GameObject newBullet = Instantiate(bulletPrefab, transform);
                newBullet.GetComponent<Bullet>().speed = -enemyData.bulletSpeed;
                newBullet.transform.parent = null;
                newBullet.layer = 6;
            }
        }
        else
        {
            shootingCont += Time.deltaTime;
        }
    }

    void ReturnToOrigin()
    {
        transform.DOKill();
        transform.position = new Vector3(transform.position.x, 8f, transform.position.z);
        transform.parent = squad.transform;
        transform.DOLocalMove(originalPosInSquad, 2).OnComplete(SetWandering);
    }

    void SetWandering()
    {
        enemyWandering = true;
    }

    public void OnDeath()
    {
        GameManagerScript.Instance.AddScore(enemyData.EXP);

        squad.RemoveEnemyFromList(gameObject);

        // TODO: Some explosion vfx
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"{enemyData.type} received DMG from {collision.gameObject.name}");
        OnDeath();
    }
}
