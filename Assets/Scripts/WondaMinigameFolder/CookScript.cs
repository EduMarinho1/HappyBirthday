using UnityEngine;

public class CookScript : MonoBehaviour
{
    public bool raddish = false;
    public bool garlic = false;
    public bool greenOnion = false;
    public bool cabbage = false;
    public bool onion = false;
    public bool shrimp = false;
    public bool gochugaru = false;
    public bool chocolate = false;
    public bool tomato = false;
    public bool cucumber = false;
    public bool cheese = false;

    public Sprite correctIngredientsSprite;
    public Sprite wrongIngredientsSprite;

    private bool alreadyChecked = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (alreadyChecked)
            return;

        alreadyChecked = true;

        GameObject food = GameObject.Find("Food");

        if (food == null)
            return;

        SpriteRenderer spriteRenderer = food.GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
            return;

        bool correctRecipe =
            shrimp &&
            onion &&
            greenOnion &&
            cabbage &&
            raddish &&
            garlic &&
            gochugaru &&

            !chocolate &&
            !tomato &&
            !cucumber &&
            !cheese;

        if (correctRecipe)
        {
            spriteRenderer.sprite = correctIngredientsSprite;
        }
        else
        {
            spriteRenderer.sprite = wrongIngredientsSprite;
        }

        Destroy(gameObject);
    }
}