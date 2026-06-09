using UnityEngine;

public class StartMauraMinigameScript : MonoBehaviour
{
    public AudioClip soundToPlay;

    public float buttonSpeed = 5f;

    private bool started = false;

    private AudioSource audioSource;

    private float musicTimer = 0f;

    public GameObject wallBlockMaura;

    public GameObject guitarHeroBaseRed;
    public GameObject guitarHeroBaseGreen;
    public GameObject guitarHeroBaseBlue;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (!started)
            return;

        musicTimer += Time.deltaTime;

        if (musicTimer >= 75.3f && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        if (musicTimer >= 76f)
        {
            if (wallBlockMaura != null)
            {
                Destroy(wallBlockMaura);
            }

            if (guitarHeroBaseRed != null)
            {
                Destroy(guitarHeroBaseRed);
            }

            if (guitarHeroBaseGreen != null)
            {
                Destroy(guitarHeroBaseGreen);
            }

            if (guitarHeroBaseBlue != null)
            {
                Destroy(guitarHeroBaseBlue);
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (started)
            return;

        started = true;

        musicTimer = 0f;

        audioSource.clip = soundToPlay;
        audioSource.Play();

        GuitarHeroButtonScript[] buttons =
            GetComponentsInChildren<GuitarHeroButtonScript>();

        foreach (GuitarHeroButtonScript button in buttons)
        {
            button.StartMoving(buttonSpeed);
        }
    }
}