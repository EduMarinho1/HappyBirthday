using UnityEngine;

public class DinahHeadScript : MonoBehaviour
{
    public DinahSnakeScript snake;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DinahMinigamePickup"))
        {
            Destroy(other.gameObject);

            if (snake != null)
            {
                snake.Grow();
            }

            return;
        }

        if (other.CompareTag("DinahTorso"))
        {
            if (snake == null)
                return;

            Transform snakeTransform = snake.transform;

            Transform firstTorso = null;

            foreach (Transform child in snakeTransform)
            {
                if (child.CompareTag("DinahTorso"))
                {
                    firstTorso = child;
                    break;
                }
            }

            if (firstTorso == null)
                return;

            if (other.transform == firstTorso)
                return;

            Destroy(snake.gameObject);
        }
    }
}