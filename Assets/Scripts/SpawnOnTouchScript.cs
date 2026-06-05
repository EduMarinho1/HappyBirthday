using UnityEngine;

public class SpawnOnTouchScript : MonoBehaviour
{
    public GameObject objectToSpawn;

    private bool alreadyTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (alreadyTriggered)
            return;

        alreadyTriggered = true;

        Transform spawnDestination = transform.Find("SpawnDestination");

        if (spawnDestination != null && objectToSpawn != null)
        {
            Instantiate(
                objectToSpawn,
                spawnDestination.position,
                Quaternion.identity
            );
        }
    }
}