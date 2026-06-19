using UnityEngine;

public class ChangeTextAndDestroy : MonoBehaviour
{
    public GameObject objectToDestroy;

    [TextArea]
    public string newText = "Kimbap";

    private TextMesh textMesh;
    private bool activated = false;

    private void Start()
    {
        textMesh = GetComponent<TextMesh>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            activated = true;

            Destroy(objectToDestroy);

            if (textMesh != null)
            {
                textMesh.text = newText;
            }
        }
    }
}