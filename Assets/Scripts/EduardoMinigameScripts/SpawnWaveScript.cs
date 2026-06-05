using UnityEngine;

public class SpawnWaveScript : MonoBehaviour
{
    public GameObject smallWavePrefab;
    public GameObject bigWavePrefab;

    public float minSpawnTime = 5f;
    public float maxSpawnTime = 10f;

    private float timer;

    void Start()
    {
        SetNextSpawnTime();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnWave();
            SetNextSpawnTime();
        }
    }

    private void SpawnWave()
    {
        GameObject waveToSpawn;

        float randomChance = Random.Range(0f, 1f);

        if (randomChance <= 0.25f)
        {
            waveToSpawn = bigWavePrefab;
        }
        else
        {
            waveToSpawn = smallWavePrefab;
        }

        Instantiate(
            waveToSpawn,
            transform.position,
            Quaternion.identity
        );
    }

    private void SetNextSpawnTime()
    {
        timer = Random.Range(minSpawnTime, maxSpawnTime);
    }
}