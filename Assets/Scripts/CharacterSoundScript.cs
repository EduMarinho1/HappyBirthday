using UnityEngine;

public class CharacterSoundScript : MonoBehaviour
{
    public AudioClip soundToPlay;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (audioSource.isPlaying)
            return;

        audioSource.PlayOneShot(soundToPlay);
    }
}