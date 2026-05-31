using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;

    float lifeTime = 5f;
    float cont = 0;

    void Update()
    {
        transform.Translate(new Vector3(0f, speed * Time.deltaTime, 0f));

        if (cont < lifeTime)
        {
            cont += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Bullet hitted: {collision.gameObject.name}");
        Destroy(gameObject);

        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.OnDeath();
        }
        else
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if(player != null) player.OnReceiveDmg();
        }

    }
}
