using UnityEngine;
using System.Collections;

public class DestroyObjectsWithSoundScript : MonoBehaviour
{
    [Header("Objects to Destroy")]
    public GameObject object1;
    public GameObject object2;

    [Header("Sound")]
    public AudioClip soundClip;

    private bool activated = false;

private void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("Touched: " + other.name);

    if (other.CompareTag("Player"))
    {
        StartCoroutine(HandleDestruction());
    }
}

    private IEnumerator HandleDestruction()
    {
        // Create temporary sound object
        GameObject soundObject = new GameObject("TemporarySound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();

        audioSource.clip = soundClip;
        audioSource.Play();

        // Destroy selected objects
        if (object1 != null)
            Destroy(object1);

        if (object2 != null)
            Destroy(object2);

        // Wait for sound to finish
        if (soundClip != null)
            yield return new WaitForSeconds(soundClip.length);

        // Destroy the temporary sound object
        Destroy(soundObject);

        // Optional: destroy this trigger object
        Destroy(gameObject);
    }
}