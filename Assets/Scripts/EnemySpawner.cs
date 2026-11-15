
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player;

    [Header("Spawn Settings")]
    [SerializeField] private float startingSpawnInterval = 2f;
    [SerializeField] private float minimumSpawnInterval = 0.4f;
    [SerializeField] private float spawnDistance = 8f;

    [Header("Difficulty")]
    [SerializeField] private float difficultyIncreaseEvery = 20f;
    [SerializeField] private float spawnIntervalReduction = 0.2f;

    [Header("Maximum Active Enemies")]
    [SerializeField] private int maxEnemiesStage1 = 8;
    [SerializeField] private int maxEnemiesStage2 = 12;
    [SerializeField] private int maxEnemiesStage3 = 16;
    [SerializeField] private int maxEnemiesStage4 = 22;

    [Header("Stage Times (Seconds)")]
    [SerializeField] private float stage2Start = 30f;
    [SerializeField] private float stage3Start = 60f;
    [SerializeField] private float stage4Start = 120f;

    [Header("End Screens (Optional)")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gameOverPanel;

    private float spawnTimer;
    private float difficultyTimer;
    private float elapsedTime;
    private float currentSpawnInterval;

    private int currentMaxEnemies;
    private int currentStage;

    private void Start()
    {
        spawnTimer = 0f;
        difficultyTimer = 0f;
        elapsedTime = 0f;

        currentSpawnInterval =
            Mathf.Max(0.1f, startingSpawnInterval);

        currentMaxEnemies =
            Mathf.Max(1, maxEnemiesStage1);

        currentStage = 1;
    }

    private void Update()
    {
        if (Time.timeScale <= 0f)
            return;

        if (winPanel != null &&
            winPanel.activeInHierarchy)
            return;

        if (gameOverPanel != null &&
            gameOverPanel.activeInHierarchy)
            return;

        elapsedTime += Time.deltaTime;
        spawnTimer += Time.deltaTime;
        difficultyTimer += Time.deltaTime;

        UpdateDifficultyStage();

        if (difficultyIncreaseEvery > 0f)
        {
            while (difficultyTimer >= difficultyIncreaseEvery)
            {
                difficultyTimer -= difficultyIncreaseEvery;
                IncreaseDifficulty();
            }
        }

        if (spawnTimer >= currentSpawnInterval)
        {
            spawnTimer = 0f;

            // No new enemies while the arena is crowded.
            if (EnemyMovement.ActiveCount < currentMaxEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    private void UpdateDifficultyStage()
    {
        int newStage;

        if (elapsedTime >= stage4Start)
        {
            newStage = 4;
            currentMaxEnemies = Mathf.Max(1, maxEnemiesStage4);
        }
        else if (elapsedTime >= stage3Start)
        {
            newStage = 3;
            currentMaxEnemies = Mathf.Max(1, maxEnemiesStage3);
        }
        else if (elapsedTime >= stage2Start)
        {
            newStage = 2;
            currentMaxEnemies = Mathf.Max(1, maxEnemiesStage2);
        }
        else
        {
            newStage = 1;
            currentMaxEnemies = Mathf.Max(1, maxEnemiesStage1);
        }

        if (newStage != currentStage)
        {
            currentStage = newStage;

            Debug.Log(
                $"Difficulty Stage {currentStage} reached. " +
                $"Maximum enemies: {currentMaxEnemies}"
            );
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null || player == null)
            return;

        float angle = Random.Range(0f, Mathf.PI * 2f);

        Vector2 randomDirection = new Vector2(
            Mathf.Cos(angle),
            Mathf.Sin(angle)
        );

        Vector2 spawnPosition =
            (Vector2)player.position +
            randomDirection * spawnDistance;

        Instantiate(
            enemyPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }

    private void IncreaseDifficulty()
    {
        currentSpawnInterval -= spawnIntervalReduction;

        currentSpawnInterval = Mathf.Max(
            currentSpawnInterval,
            Mathf.Max(0.1f, minimumSpawnInterval)
        );

        Debug.Log(
            $"Spawn interval: {currentSpawnInterval:F1}s"
        );
    }
}
