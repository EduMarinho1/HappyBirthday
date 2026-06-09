using UnityEngine;

public class MarkPointsScript : MonoBehaviour
{
    public Sprite missSprite;
    public Sprite goodSprite;
    public Sprite greatSprite;
    public Sprite excellentSprite;

    private GuitarHeroLineScript redLine1;
    private GuitarHeroLineScript redLine2;
    private GuitarHeroLineScript redLine3;

    private GuitarHeroLineScript greenLine1;
    private GuitarHeroLineScript greenLine2;
    private GuitarHeroLineScript greenLine3;

    private GuitarHeroLineScript blueLine1;
    private GuitarHeroLineScript blueLine2;
    private GuitarHeroLineScript blueLine3;

    private SpriteRenderer redFeedback;
    private SpriteRenderer greenFeedback;
    private SpriteRenderer blueFeedback;

    private float redFeedbackTimer;
    private float greenFeedbackTimer;
    private float blueFeedbackTimer;

    private float redCooldown = 0f;
    private float greenCooldown = 0f;
    private float blueCooldown = 0f;

    void Start()
    {
        redLine1 = transform.Find("GuitarHeroBaseRed/Line1").GetComponent<GuitarHeroLineScript>();
        redLine2 = transform.Find("GuitarHeroBaseRed/Line2").GetComponent<GuitarHeroLineScript>();
        redLine3 = transform.Find("GuitarHeroBaseRed/Line3").GetComponent<GuitarHeroLineScript>();

        greenLine1 = transform.Find("GuitarHeroBaseGreen/Line1").GetComponent<GuitarHeroLineScript>();
        greenLine2 = transform.Find("GuitarHeroBaseGreen/Line2").GetComponent<GuitarHeroLineScript>();
        greenLine3 = transform.Find("GuitarHeroBaseGreen/Line3").GetComponent<GuitarHeroLineScript>();

        blueLine1 = transform.Find("GuitarHeroBaseBlue/Line1").GetComponent<GuitarHeroLineScript>();
        blueLine2 = transform.Find("GuitarHeroBaseBlue/Line2").GetComponent<GuitarHeroLineScript>();
        blueLine3 = transform.Find("GuitarHeroBaseBlue/Line3").GetComponent<GuitarHeroLineScript>();

        redFeedback =
            transform.Find("GuitarHeroBaseRed/FeedbackLocation")
            .GetComponent<SpriteRenderer>();

        greenFeedback =
            transform.Find("GuitarHeroBaseGreen/FeedbackLocation")
            .GetComponent<SpriteRenderer>();

        blueFeedback =
            transform.Find("GuitarHeroBaseBlue/FeedbackLocation")
            .GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        redCooldown -= Time.deltaTime;
        greenCooldown -= Time.deltaTime;
        blueCooldown -= Time.deltaTime;

        UpdateFeedbackTimers();

        CheckFireTrace(
            redLine1,
            redLine2,
            redLine3,
            KeyCode.I,
            "Red",
            redFeedback,
            ref redFeedbackTimer
        );

        CheckFireTrace(
            greenLine1,
            greenLine2,
            greenLine3,
            KeyCode.J,
            "Green",
            greenFeedback,
            ref greenFeedbackTimer
        );

        CheckFireTrace(
            blueLine1,
            blueLine2,
            blueLine3,
            KeyCode.N,
            "Blue",
            blueFeedback,
            ref blueFeedbackTimer
        );

        if (Input.GetKeyDown(KeyCode.I) && redCooldown <= 0)
        {
            CheckColorAndDestroy(
                redLine1,
                redLine2,
                redLine3,
                "Red",
                redFeedback,
                ref redCooldown,
                ref redFeedbackTimer
            );
        }

        if (Input.GetKeyDown(KeyCode.J) && greenCooldown <= 0)
        {
            CheckColorAndDestroy(
                greenLine1,
                greenLine2,
                greenLine3,
                "Green",
                greenFeedback,
                ref greenCooldown,
                ref greenFeedbackTimer
            );
        }

        if (Input.GetKeyDown(KeyCode.N) && blueCooldown <= 0)
        {
            CheckColorAndDestroy(
                blueLine1,
                blueLine2,
                blueLine3,
                "Blue",
                blueFeedback,
                ref blueCooldown,
                ref blueFeedbackTimer
            );
        }
    }

    private void UpdateFeedbackTimers()
    {
        redFeedbackTimer -= Time.deltaTime;
        greenFeedbackTimer -= Time.deltaTime;
        blueFeedbackTimer -= Time.deltaTime;

        if (redFeedbackTimer <= 0)
        {
            redFeedback.sprite = null;
        }

        if (greenFeedbackTimer <= 0)
        {
            greenFeedback.sprite = null;
        }

        if (blueFeedbackTimer <= 0)
        {
            blueFeedback.sprite = null;
        }
    }

    private void CheckFireTrace(
        GuitarHeroLineScript line1,
        GuitarHeroLineScript line2,
        GuitarHeroLineScript line3,
        KeyCode key,
        string color,
        SpriteRenderer feedbackRenderer,
        ref float feedbackTimer
    )
    {
        GameObject fireTrace = null;

        if (
            line1.currentButton != null &&
            line1.currentButton.CompareTag("FireTrace") &&
            line1.currentButton == line2.currentButton
        )
        {
            fireTrace = line1.currentButton;
        }
        else if (
            line1.currentButton != null &&
            line1.currentButton.CompareTag("FireTrace") &&
            line1.currentButton == line3.currentButton
        )
        {
            fireTrace = line1.currentButton;
        }
        else if (
            line2.currentButton != null &&
            line2.currentButton.CompareTag("FireTrace") &&
            line2.currentButton == line3.currentButton
        )
        {
            fireTrace = line2.currentButton;
        }

        if (fireTrace == null)
            return;

        if (Input.GetKey(key))
        {
            Debug.Log("Excellent " + color);

            feedbackRenderer.sprite = excellentSprite;
        }
        else
        {
            Debug.Log("Missed " + color);

            feedbackRenderer.sprite = missSprite;
        }

        feedbackTimer = 0.5f;

        Destroy(fireTrace);
    }

    private void CheckColorAndDestroy(
        GuitarHeroLineScript line1,
        GuitarHeroLineScript line2,
        GuitarHeroLineScript line3,
        string color,
        SpriteRenderer feedbackRenderer,
        ref float cooldown,
        ref float feedbackTimer
    )
    {
        if (
            (line1.currentButton != null &&
             line1.currentButton.CompareTag("FireTrace")) ||

            (line2.currentButton != null &&
             line2.currentButton.CompareTag("FireTrace")) ||

            (line3.currentButton != null &&
             line3.currentButton.CompareTag("FireTrace"))
        )
        {
            return;
        }

        int count = 0;

        if (line1.onLine) count++;
        if (line2.onLine) count++;
        if (line3.onLine) count++;

        if (count == 0)
        {
            Debug.Log("Missed " + color);

            feedbackRenderer.sprite = missSprite;
            feedbackTimer = 0.5f;

            cooldown = 0.5f;

            return;
        }

        if (count == 1)
        {
            Debug.Log("Good " + color);

            feedbackRenderer.sprite = goodSprite;
            feedbackTimer = 0.5f;
        }
        else if (count == 2)
        {
            Debug.Log("Great " + color);

            feedbackRenderer.sprite = greatSprite;
            feedbackTimer = 0.5f;
        }
        else if (count == 3)
        {
            Debug.Log("Excellent " + color);

            feedbackRenderer.sprite = excellentSprite;
            feedbackTimer = 0.5f;
        }

        if (line1.currentButton != null)
        {
            Destroy(line1.currentButton);
        }

        if (line2.currentButton != null)
        {
            Destroy(line2.currentButton);
        }

        if (line3.currentButton != null)
        {
            Destroy(line3.currentButton);
        }
    }
}