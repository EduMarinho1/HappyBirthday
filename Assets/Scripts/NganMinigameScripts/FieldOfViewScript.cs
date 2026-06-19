using UnityEngine;
using System.Collections;

public class FieldOfViewScript : MonoBehaviour
{
    public GuardVisionScript guard;

    private bool playerInside = false;
    private bool detectionRunning = false;

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
        if (detectionRunning)
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

        StartCoroutine(DetectionRoutine());
    }

    private IEnumerator DetectionRoutine()
    {
        detectionRunning = true;

        PlayerScript player =
            GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerScript>();

        player.isBeingPushed = true;

        guard.TriggerDetection();

        float duration =
            guard.caughtSound != null
            ? guard.caughtSound.length
            : 1f;

        yield return new WaitForSeconds(duration);

        player.isBeingPushed = false;

        detectionRunning = false;
    }
}