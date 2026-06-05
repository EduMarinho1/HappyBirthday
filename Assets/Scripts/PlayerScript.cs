using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 3f;

    private bool onFastPad = false;
    private bool onSlowPad = false;

    private Animator animator;
    private string currentAnimation;

    private enum Direction
    {
        Down,
        Up,
        Left,
        Right
    }

    private Direction lastDirection = Direction.Down;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float currentSpeed = speed;

        if (onSlowPad)
        {
            currentSpeed = speed / 3f;
        }
        else if (onFastPad)
        {
            currentSpeed = speed * 2f;
        }

        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movement.y += 1;
            lastDirection = Direction.Up;
            PlayAnimation("WalkUp");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement.y -= 1;
            lastDirection = Direction.Down;
            PlayAnimation("WalkDown");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movement.x -= 1;
            lastDirection = Direction.Left;
            PlayAnimation("WalkLeft");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement.x += 1;
            lastDirection = Direction.Right;
            PlayAnimation("WalkRight");
        }
        else
        {
            switch (lastDirection)
            {
                case Direction.Up:
                    PlayAnimation("IdleUp");
                    break;

                case Direction.Down:
                    PlayAnimation("IdleDown");
                    break;

                case Direction.Left:
                    PlayAnimation("IdleLeft");
                    break;

                case Direction.Right:
                    PlayAnimation("IdleRight");
                    break;
            }
        }

        transform.position += movement.normalized * currentSpeed * Time.deltaTime;
    }

    private void PlayAnimation(string animationName)
    {
        if (currentAnimation == animationName)
            return;

        currentAnimation = animationName;
        animator.Play(animationName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FastPad"))
        {
            onFastPad = true;
        }

        if (other.CompareTag("SlowPad"))
        {
            onSlowPad = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FastPad"))
        {
            onFastPad = false;
        }

        if (other.CompareTag("SlowPad"))
        {
            onSlowPad = false;
        }
    }
}