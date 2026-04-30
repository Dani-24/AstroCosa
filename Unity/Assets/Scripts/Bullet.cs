using UnityEngine;
using static UnityEditor.PlayerSettings;

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
}
