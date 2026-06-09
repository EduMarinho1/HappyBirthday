using UnityEngine;
using System.Collections.Generic;

public class DinahSnakeScript : MonoBehaviour
{
    public float speed = 3f;

    public GameObject torsoPrefab;

    public float segmentSpacing = 0.3f;

    public GameObject objectToDestroy1;
    public GameObject objectToDestroy2;
    public GameObject objectToDestroy3;

    private List<Transform> segments = new List<Transform>();

    private Vector2 direction = Vector2.right;

    private List<Vector3> previousPositions = new List<Vector3>();

    public bool gameStarted = false;

    void Start()
    {
        RefreshSegments();
    }

    void Update()
    {
        if (!gameStarted)
            return;

        HandleInput();

        RefreshSegments();

        if (segments.Count == 0)
            return;

        previousPositions.Clear();

        foreach (Transform segment in segments)
        {
            previousPositions.Add(segment.position);
        }

        segments[0].position +=
            (Vector3)(direction * speed * Time.deltaTime);

        for (int i = 1; i < segments.Count; i++)
        {
            Vector3 directionToPrevious =
                (segments[i].position - previousPositions[i - 1]).normalized;

            Vector3 targetPosition =
                previousPositions[i - 1] +
                directionToPrevious * segmentSpacing;

            segments[i].position = Vector3.Lerp(
                segments[i].position,
                targetPosition,
                10f * Time.deltaTime
            );
        }
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = Vector2.right;
        }
    }

    private void RefreshSegments()
    {
        segments.Clear();

        foreach (Transform child in transform)
        {
            if (child.CompareTag("DinahHead"))
            {
                segments.Add(child);
                break;
            }
        }

        foreach (Transform child in transform)
        {
            if (child.CompareTag("DinahTorso"))
            {
                segments.Add(child);
            }
        }
    }

    public void Grow()
    {
        RefreshSegments();

        Transform lastSegment =
            segments[segments.Count - 1];

        Instantiate(
            torsoPrefab,
            lastSegment.position,
            Quaternion.identity,
            transform
        );

        RefreshSegments();
    }

    public void KillSnake()
    {
        if (objectToDestroy1 != null)
        {
            Destroy(objectToDestroy1);
        }

        if (objectToDestroy2 != null)
        {
            Destroy(objectToDestroy2);
        }

        if (objectToDestroy3 != null)
        {
            Destroy(objectToDestroy3);
        }

        Destroy(gameObject);
    }
}