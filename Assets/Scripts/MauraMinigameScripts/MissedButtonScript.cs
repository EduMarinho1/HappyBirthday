using UnityEngine;

public class MissedButtonScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.name);

        if (other.CompareTag("Button"))
        {
            Destroy(other.gameObject);
        }
    }
}