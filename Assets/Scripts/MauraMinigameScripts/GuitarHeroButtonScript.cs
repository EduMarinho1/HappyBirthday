using UnityEngine;

public class GuitarHeroButtonScript : MonoBehaviour
{
    private bool moving = false;

    private float speed;

    void Update()
    {
        if (!moving)
            return;

        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    public void StartMoving(float moveSpeed)
    {
        speed = moveSpeed;
        moving = true;
    }
}