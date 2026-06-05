using UnityEngine;

public class StartRaceScript : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;

    private SpriteRenderer spriteRenderer;

    private bool raceStarted = false;
    private float timer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite1;
    }

    void Update()
    {
        if (!raceStarted)
            return;

        timer += Time.deltaTime;

        if (timer >= 3f && timer < 6f)
        {
            spriteRenderer.sprite = sprite2;
        }
        else if (timer >= 6f && timer < 9f)
        {
            spriteRenderer.sprite = sprite3;
        }
        else if (timer >= 9f && timer < 12f)
        {
            spriteRenderer.sprite = sprite4;
        }
        else if (timer >= 12f)
        {
            GameObject robMinigame = GameObject.Find("RobMinigame");

            if (robMinigame != null)
            {
                RobRunScript robScript =
                    robMinigame.GetComponent<RobRunScript>();

                if (robScript != null)
                {
                    robScript.StartRunning();
                }
            }

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (raceStarted)
            return;

        raceStarted = true;
        timer = 0f;
    }
}