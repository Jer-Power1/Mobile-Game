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

    private float spawnTimer;
    private float difficultyTimer;
    private float currentSpawnInterval;

    private void Start()
    {
        currentSpawnInterval = startingSpawnInterval;
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        difficultyTimer += Time.deltaTime;

        if (spawnTimer >= currentSpawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }

        if (difficultyTimer >= difficultyIncreaseEvery)
        {
            IncreaseDifficulty();
            difficultyTimer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null || player == null)
            return;

        Vector2 randomDirection =
            Random.insideUnitCircle.normalized;

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
            minimumSpawnInterval
        );

        Debug.Log(
            $"Difficulty increased! Spawn interval: " +
            $"{currentSpawnInterval:F1}s"
        );
    }
}