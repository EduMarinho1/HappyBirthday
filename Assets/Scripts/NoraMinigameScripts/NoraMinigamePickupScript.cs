using UnityEngine;

public class NoraMinigamePickupScript : MonoBehaviour
{
    public AudioClip audioClip;

    public GameObject objectToDestroy1;
    public GameObject objectToDestroy2;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (activated)
            return;

        activated = true;

        if (audioClip != null)
        {
            GameObject audioObject = new GameObject("TemporaryAudio");

            AudioSource audioSource =
                audioObject.AddComponent<AudioSource>();

            audioSource.clip = audioClip;
            audioSource.Play();

            Destroy(
                audioObject,
                audioClip.length
            );
        }

        if (objectToDestroy1 != null)
        {
            Destroy(objectToDestroy1);
        }

        if (objectToDestroy2 != null)
        {
            Destroy(objectToDestroy2);
        }

        Destroy(gameObject);
    }
}