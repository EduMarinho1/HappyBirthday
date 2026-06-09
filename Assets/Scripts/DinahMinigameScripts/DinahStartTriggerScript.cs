using UnityEngine;

public class DinahStartTriggerScript : MonoBehaviour
{
    public DinahSnakeScript snake;

    private bool playerInside = false;

    private bool activated = false;

    void Update()
    {
        if (activated)
            return;

        if (!playerInside)
            return;

        if (
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow)
        )
        {
            activated = true;

            snake.gameStarted = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = false;
    }
}