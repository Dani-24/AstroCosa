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

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enemyData.sprite;

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

            // TODO: Special Attack instead of shooting

            // Uses special moves according to the enemy type.
            // And attacks more depending on the difficulty (both shooting and special attacks).
            // If it's the last enemy alive, always uses special moves


            // On idle, it doesn't move, when wants to move, unparents from squad, attacks, and then
            // returns to it's original position and reparents itself

            shootingCont = 0;
            CalculateNextShootCd();

            GameObject newBullet = Instantiate(bulletPrefab, transform);
            newBullet.GetComponent<Bullet>().speed *= -1;
            newBullet.transform.parent = null;
            newBullet.layer = 6;
        }
        else
        {
            shootingCont += Time.deltaTime;
        }
    }

    void OnDeath()
    {
        GameManagerScript.Instance.score += enemyData.EXP;

        squad.RemoveEnemyFromList(gameObject);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"{enemyData.type} received DMG from {collision.gameObject.name}");

        OnDeath();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{enemyData.type} received BULLET DMG from {collision.gameObject.name}");
        Destroy(collision.gameObject);
        OnDeath();
    }
}
