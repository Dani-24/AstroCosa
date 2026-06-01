using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;

    float lifeTime = 5f;
    float cont = 0;

    public AudioClip[] audios;
    public float pitchVariation = 0.1f;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audios[Random.Range(0, audios.Length)];
        audioSource.pitch = Random.Range(1f - pitchVariation, 1f + pitchVariation);
        audioSource.Play();
    }

    void Update()
    {
        if (speed == 0) return;

        transform.position += speed * Time.deltaTime * transform.up;

        if (cont < lifeTime) cont += Time.deltaTime;
        else Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Bullet hitted: {collision.gameObject.name}");
        Destroy(gameObject);

        Character ch = collision.GetComponent<Character>();

        if (ch != null)
            ch.OnReceiveDmg();
    }
}
