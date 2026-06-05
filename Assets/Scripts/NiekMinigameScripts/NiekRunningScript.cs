using UnityEngine;

public class NiekRunningScript : MonoBehaviour
{
    public float speed = 1f;
    public float directionChangeInterval = 2f;

    private Vector2 direction;
    private float timer;

    private bool caught = false;

    void Start()
    {
        ChooseNewDirection();
    }

    void Update()
    {
        if (caught)
            return;

        transform.position += (Vector3)(direction.normalized * speed * Time.deltaTime);

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            ChooseNewDirection();
        }
    }

    private void ChooseNewDirection()
    {
        int randomDirection = Random.Range(0, 8);

        switch (randomDirection)
        {
            case 0:
                direction = Vector2.up;
                break;

            case 1:
                direction = Vector2.down;
                break;

            case 2:
                direction = Vector2.left;
                break;

            case 3:
                direction = Vector2.right;
                break;

            case 4:
                direction = new Vector2(1, 1);
                break;

            case 5:
                direction = new Vector2(-1, 1);
                break;

            case 6:
                direction = new Vector2(1, -1);
                break;

            case 7:
                direction = new Vector2(-1, -1);
                break;
        }

        timer = directionChangeInterval;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        caught = true;
    }
}