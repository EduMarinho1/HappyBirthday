using UnityEngine;
using System.Collections;

public class GuardVisionScript : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite eyesClosedSprite;
    public Sprite eyesOpenSprite;
    public Sprite mouthOpenSprite;

    [Header("Audio")]
    public AudioClip caughtSound;
    public AudioClip reachedCharacterSound;

    [Header("Objects To Destroy When Reached")]
    public GameObject objectToDestroy1;
    public GameObject objectToDestroy2;

    [Header("Timing")]
    public float minClosedTime = 2f;
    public float maxClosedTime = 5f;

    public float minOpenTime = 2.5f;
    public float maxOpenTime = 3.5f;

    public float gracePeriod = 0.3f;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    private bool detectionTriggered = false;
    private bool playerReachedCharacter = false;

    public bool CanDetectPlayer { get; private set; }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;

        if (eyesClosedSprite != null)
        {
            spriteRenderer.sprite = eyesClosedSprite;
        }

        StartCoroutine(VisionLoop());
    }

    private IEnumerator VisionLoop()
    {
        while (!playerReachedCharacter)
        {
            // Eyes closed
            CanDetectPlayer = false;
            detectionTriggered = false;

            if (eyesClosedSprite != null)
            {
                spriteRenderer.sprite = eyesClosedSprite;
            }

            yield return new WaitForSeconds(
                Random.Range(minClosedTime, maxClosedTime)
            );

            if (playerReachedCharacter)
            {
                yield break;
            }

            // Eyes open
            if (eyesOpenSprite != null)
            {
                spriteRenderer.sprite = eyesOpenSprite;
            }

            yield return new WaitForSeconds(gracePeriod);

            CanDetectPlayer = true;

            float remainingOpenTime =
                Random.Range(minOpenTime, maxOpenTime) - gracePeriod;

            float timer = 0f;

            while (timer < remainingOpenTime)
            {
                if (detectionTriggered || playerReachedCharacter)
                {
                    break;
                }

                timer += Time.deltaTime;
                yield return null;
            }

            CanDetectPlayer = false;
        }
    }

    public void TriggerDetection()
    {
        if (detectionTriggered)
            return;

        if (playerReachedCharacter)
            return;

        detectionTriggered = true;

        StopAllCoroutines();
        StartCoroutine(DetectionRoutine());
    }

    private IEnumerator DetectionRoutine()
    {
        CanDetectPlayer = false;

        if (mouthOpenSprite != null)
        {
            spriteRenderer.sprite = mouthOpenSprite;
        }

        if (caughtSound != null)
        {
            audioSource.PlayOneShot(caughtSound);

            yield return new WaitForSeconds(caughtSound.length);
        }

        if (!playerReachedCharacter)
        {
            if (eyesClosedSprite != null)
            {
                spriteRenderer.sprite = eyesClosedSprite;
            }

            StartCoroutine(VisionLoop());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Something touched the character: " + collision.gameObject.name);

        if (!collision.gameObject.CompareTag("Player"))
            return;

        Debug.Log("Player reached character!");

        playerReachedCharacter = true;

        StopAllCoroutines();

        CanDetectPlayer = false;

        if (mouthOpenSprite != null)
        {
            spriteRenderer.sprite = mouthOpenSprite;
        }

        if (reachedCharacterSound != null)
        {
            audioSource.PlayOneShot(reachedCharacterSound);
        }

        if (objectToDestroy1 != null)
        {
            Destroy(objectToDestroy1);
        }

        if (objectToDestroy2 != null)
        {
            Destroy(objectToDestroy2);
        }
    }
}