using UnityEngine;

public class DinahPickupSpawnerScript : MonoBehaviour
{
    public GameObject pickupPrefab;

    public float spawnInterval = 3f;

    private float timer;

    private BoxCollider2D spawnArea;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider2D>();

        timer = spawnInterval;
    }

void Update()
{
    timer -= Time.deltaTime;

    if (timer <= 0f)
    {
        Debug.Log("Spawning pickup");

        SpawnPickup();

        timer = spawnInterval;
    }
}

    private void SpawnPickup()
    {
        if (pickupPrefab == null)
            return;

        Bounds bounds = spawnArea.bounds;

        float randomX = Random.Range(
            bounds.min.x,
            bounds.max.x
        );

        float randomY = Random.Range(
            bounds.min.y,
            bounds.max.y
        );

        Vector3 spawnPosition = new Vector3(
            randomX,
            randomY,
            0f
        );

        Instantiate(
            pickupPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}