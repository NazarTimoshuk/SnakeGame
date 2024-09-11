using UnityEngine;
using System.Collections.Generic;

public class SnakeMovement : MonoBehaviour
{
    public List<Transform> snakeSegments;
    public Vector2Int direction = Vector2Int.right;
    private float moveTimer = 0f;
    private float moveInterval = 0.3f;

    SnakeGame snakeGame;

    public static SnakeMovement instance;

    private void Start()
    {
        instance = this;
    }

    public void Initialize(Transform headPrefab, Transform segmentPrefab, int initialLength, Vector2 startPos, SnakeGame snakeGame)
    {
        snakeSegments = new List<Transform>();
        snakeSegments.Add(Instantiate(headPrefab));
        snakeSegments[0].position = startPos;
        this.snakeGame = snakeGame;
        for (int i = 1; i < initialLength; i++)
        {
            snakeSegments.Add(Instantiate(segmentPrefab));
            snakeSegments[i].position = new Vector2(i + startPos.x, startPos.y);
        }
    }

    void Update()
    {
        if (snakeGame.gameOver) return;

        moveTimer += Time.deltaTime;
        if (moveTimer >= moveInterval)
        {
            MoveSnake();
            moveTimer = 0f;
        }
    }

    public void HandleInput(Vector2Int dir)
    {
        Vector2Int oldDir = direction;

        direction = dir;

        if (snakeSegments[1].transform.position == snakeSegments[0].transform.position + new Vector3(direction.x, direction.y, 0))
        {
            direction = oldDir;
        }
    }

    void MoveSnake()
    {
        Vector3 nextPos = snakeSegments[0].position;
        Vector3 currentPos;
        snakeSegments[0].position += new Vector3(direction.x, direction.y, 0);

        for (int i = 1; i < snakeSegments.Count; i++)
        {
            currentPos = snakeSegments[i].position;
            snakeSegments[i].position = nextPos;
            nextPos = currentPos;
        }
    }

    public void GrowSnake(Transform segmentPrefab)
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = snakeSegments[snakeSegments.Count - 1].position;
        snakeSegments.Add(segment);
    }
}