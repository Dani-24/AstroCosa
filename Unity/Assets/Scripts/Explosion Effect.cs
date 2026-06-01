using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class ExplosionEffect : MonoBehaviour
{
    public AudioClip[] audios;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;

    [Header("Values")]
    public float finalScale = 2.0f;
    public float scaleDuration = 1.0f;
    public float pitchVariation = 0.1f;
    public float lifetime = 3.0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        audioSource.clip = audios[Random.Range(0, audios.Length)];
        audioSource.pitch = Random.Range(1f - pitchVariation, 1f + pitchVariation);
        audioSource.Play();

        transform.DOScale(finalScale, scaleDuration).OnComplete(() => { spriteRenderer.sprite = null; });

        Destroy(gameObject, lifetime);
    }
}
