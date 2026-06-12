using UnityEngine;

public class BreedButtonScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        BreedingManagerScript.Instance.TryBreed(
            gameObject.name,
            gameObject
        );
    }
}