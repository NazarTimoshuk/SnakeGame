using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SnakeGame : MonoBehaviour
{
    public int width = 20;
    public int height = 20;
    public Transform headPrefab;
    public Transform segmentPrefab;
    public Transform foodPrefab;
    public GameObject borderTile;

    private SnakeMovement snakeMovement;
    private Vector2Int foodPosition;
    public bool gameOver = false;

    [SerializeField] Camera mainCamera;

    void Start()
    {
        snakeMovement = GetComponent<SnakeMovement>();
        snakeMovement.Initialize(headPrefab, segmentPrefab, 4, new Vector2(width / 2, height / 2),this);

        
        BuildMap();
        SpawnFood();
        SetCameraPos();
    }

    public void SetCameraPos()
    {
        if (width % 2 == 0)
            mainCamera.transform.position = new Vector3(width / 2 - 0.5f, 0, -10);
        else
            mainCamera.transform.position = new Vector3(width / 2, 0, -10);

        mainCamera.orthographicSize = width + 5;
    }
    void Update()
    {
        if (gameOver) return;

        CheckCollisions();
    }

    void BuildMap()
    {
        for (int i = -1; i <= width; i++)
        {
            for (int j = -1; j <= height; j++)
            {
                if (i == -1 || i == width  || j == -1 || j == height)
                {
                    Instantiate(borderTile, new Vector3(i, j, 0), Quaternion.identity).transform.SetParent(gameObject.transform);
                }
            }
        }
    }

    void CheckCollisions()
    {
        Vector3 headPos = snakeMovement.snakeSegments[0].position;


        if (headPos.x >= width || headPos.x < 0 || headPos.y >= height || headPos.y < 0)
        {
            EndGame();
        }

        for (int i = 1; i < snakeMovement.snakeSegments.Count; i++)
        {
            if (headPos == snakeMovement.snakeSegments[i].position)
            {
                EndGame();
            }
        }

        if (headPos == new Vector3(foodPosition.x, foodPosition.y, 0))
        {
            snakeMovement.GrowSnake(segmentPrefab);
            SpawnFood();
        }
    }

    void SpawnFood()
    {
        foodPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        foodPrefab.position = new Vector3(foodPosition.x, foodPosition.y, 0);
    }

    void EndGame()
    {
        gameOver = true;
        Debug.Log("Game Over!");
    }
}
