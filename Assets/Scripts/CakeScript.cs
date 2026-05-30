using UnityEngine;

public class CakeScript : MonoBehaviour
{
    public int candles = 0;

    public Sprite[] cakeSprites;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateCakeSprite();
    }

    public void AddCandle()
    {
        if (candles < 22)
        {
            candles++;
            UpdateCakeSprite();
        }
    }

    private void UpdateCakeSprite()
    {
        spriteRenderer.sprite = cakeSprites[candles];
    }
}