using UnityEngine;

public class GuitarHeroLineScript : MonoBehaviour
{
    public bool onLine = false;

    public GameObject currentButton = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (
            other.CompareTag("Button") ||
            other.CompareTag("FireTrace")
        )
        {
            onLine = true;
            currentButton = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (
            other.CompareTag("Button") ||
            other.CompareTag("FireTrace")
        )
        {
            onLine = false;

            if (currentButton == other.gameObject)
            {
                currentButton = null;
            }
        }
    }
}