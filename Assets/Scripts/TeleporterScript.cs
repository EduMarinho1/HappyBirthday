using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    private Transform destination;

    void Start()
    {
        destination = transform.Find("Destination");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        other.transform.position = destination.position;
    }
}