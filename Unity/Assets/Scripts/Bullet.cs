using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;

    float lifeTime = 5f;
    float cont = 0;

    void Update()
    {
        transform.position += speed * Time.deltaTime * new Vector3(0f, transform.position.y, 0f);
    
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
