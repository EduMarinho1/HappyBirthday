using UnityEngine;
using System.Collections;

public class FieldOfViewScript : MonoBehaviour
{
    public GuardVisionScript guard;

    [Header("Push Settings")]
    public float pushSpeed = 3f;

    private GameObject player;
    private bool playerInside = false;
    private bool currentlyPushing = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    private void Update()
    {
        if (currentlyPushing)
            return;

        if (!playerInside)
            return;

        if (!guard.CanDetectPlayer)
            return;

        bool moving =
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D);

        if (!moving)
            return;

        StartCoroutine(PushPlayerRoutine());
    }

    private IEnumerator PushPlayerRoutine()
    {
        currentlyPushing = true;

        guard.TriggerDetection();

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        float pushDuration = guard.caughtSound != null
            ? guard.caughtSound.length
            : 0.5f;

        float timer = 0f;

        while (timer < pushDuration)
        {
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(
                    -pushSpeed,
                    rb.linearVelocity.y
                );
            }

            timer += Time.deltaTime;
            yield return null;
        }

        currentlyPushing = false;
    }
}