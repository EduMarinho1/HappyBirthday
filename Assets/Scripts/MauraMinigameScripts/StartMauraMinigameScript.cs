using UnityEngine;

public class StartMauraMinigameScript : MonoBehaviour
{
    public AudioClip soundToPlay;

    public float buttonSpeed = 5f;

    private bool started = false;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (started)
            return;

        started = true;

        if (soundToPlay != null)
        {
            audioSource.PlayOneShot(soundToPlay);
        }

        GuitarHeroButtonScript[] buttons =
            GetComponentsInChildren<GuitarHeroButtonScript>();

        foreach (GuitarHeroButtonScript button in buttons)
        {
            button.StartMoving(buttonSpeed);
        }
    }
}