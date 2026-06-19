using UnityEngine;

public class IlseMinigameDestroyObjectsScript : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(object1);
            Destroy(object2);
            Destroy(object3);
        }
    }
}