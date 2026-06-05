using UnityEngine;

public class WaveSpeedScript : MonoBehaviour
{
    private float speed;

    void Start()
    {
        speed = Random.Range(2f, 4f);
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("WaveDestroyer"))
            return;

        Destroy(gameObject);
    }
}