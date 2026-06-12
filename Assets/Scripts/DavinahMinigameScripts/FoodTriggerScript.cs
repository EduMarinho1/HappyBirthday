using UnityEngine;

public class FoodTriggerScript : MonoBehaviour
{
    private SpriteRenderer choiceSprite;

    private void Start()
    {
        Transform child = transform.GetChild(0);

        choiceSprite = child.GetComponent<SpriteRenderer>();

        if (choiceSprite != null)
        {
            choiceSprite.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        BreedingManagerScript.Instance.SelectFood(this);
    }

    public void SetSelected(bool selected)
    {
        if (choiceSprite != null)
        {
            choiceSprite.enabled = selected;
        }
    }
}