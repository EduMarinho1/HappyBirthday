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

    [Header("Timing")]
    public float minClosedTime = 2f;
    public float maxClosedTime = 5f;

    public float minOpenTime = 2.5f;
    public float maxOpenTime = 3.5f;

    public float gracePeriod = 0.3f;

    private SpriteRenderer spriteRenderer;

    private bool detectionTriggered = false;

    public bool CanDetectPlayer { get; private set; }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(VisionLoop());
    }

    private IEnumerator VisionLoop()
    {
        while (true)
        {
            // Eyes closed
            CanDetectPlayer = false;
            detectionTriggered = false;
            spriteRenderer.sprite = eyesClosedSprite;

            yield return new WaitForSeconds(
                Random.Range(minClosedTime, maxClosedTime)
            );

            // Eyes open
            spriteRenderer.sprite = eyesOpenSprite;

            // Grace period
            yield return new WaitForSeconds(gracePeriod);

            CanDetectPlayer = true;

            float remainingOpenTime =
                Random.Range(minOpenTime, maxOpenTime) - gracePeriod;

            float timer = 0f;

            while (timer < remainingOpenTime)
            {
                if (detectionTriggered)
                    break;

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

        detectionTriggered = true;

        StopAllCoroutines();
        StartCoroutine(DetectionRoutine());
    }

    private IEnumerator DetectionRoutine()
    {
        CanDetectPlayer = false;

        spriteRenderer.sprite = mouthOpenSprite;

        if (caughtSound != null)
        {
            AudioSource.PlayClipAtPoint(caughtSound, transform.position);

            yield return new WaitForSeconds(caughtSound.length);
        }

        spriteRenderer.sprite = eyesClosedSprite;

        StartCoroutine(VisionLoop());
    }
}