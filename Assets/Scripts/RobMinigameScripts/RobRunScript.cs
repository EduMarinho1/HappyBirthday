using UnityEngine;

public class RobRunScript : MonoBehaviour
{
    public float speed = 5f;

    private bool running = false;
    private float timer = 0f;

    void Update()
    {
        if (!running)
            return;

        transform.position += Vector3.right * speed * Time.deltaTime;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            running = false;
        }
    }

    public void StartRunning()
    {
        running = true;
        timer = 10.9f;
    }
}