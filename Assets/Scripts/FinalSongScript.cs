using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
public class FinalSongScript : MonoBehaviour
{
    [Header("Settings")]
    public AudioClip audioClip;
    public int requiredCharacters = 16;

    private HashSet<GameObject> touchedCharacters = new HashSet<GameObject>();
    private bool playerTouched = false;
    private bool activated = false;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Touched: " + other.name + " Tag: " + other.tag);

        if (activated)
            return;

        if (other.CompareTag("Character"))
        {
            touchedCharacters.Add(other.gameObject);
        }

        if (other.CompareTag("Player"))
        {
            playerTouched = true;
        }

        CheckConditions();
    }

private void CheckConditions()
{
    Debug.Log("Characters: " + touchedCharacters.Count +
              " Player: " + playerTouched);

    if (activated)
        return;

    if (touchedCharacters.Count >= requiredCharacters && playerTouched)
    {
        Debug.Log("PLAYING SONG!");
        activated = true;
        StartCoroutine(PlayAndDestroy());
    }
}

    private IEnumerator PlayAndDestroy()
    {
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();

            yield return new WaitForSeconds(audioClip.length);
        }

        Destroy(gameObject);
    }
}