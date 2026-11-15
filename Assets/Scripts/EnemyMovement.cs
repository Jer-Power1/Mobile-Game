
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float stoppingDistance = 0.65f;

    [Header("Enemy Separation")]
    [SerializeField] private float separationRadius = 1.5f;
    [SerializeField] private float separationStrength = 1.5f;

    private Transform player;
    private Rigidbody2D rb;

    private static readonly List<EnemyMovement> activeEnemies =
        new List<EnemyMovement>();

    private static int nextEnemyOrder;
    private int enemyOrder;

    public static int ActiveCount => activeEnemies.Count;

    [RuntimeInitializeOnLoadMethod(
        RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetEnemyList()
    {
        activeEnemies.Clear();
        nextEnemyOrder = 0;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        rb.interpolation =
            RigidbodyInterpolation2D.Interpolate;
    }

    private void OnEnable()
    {
        if (!activeEnemies.Contains(this))
        {
            enemyOrder = nextEnemyOrder++;
            activeEnemies.Add(this);
        }
    }

    private void OnDisable()
    {
        activeEnemies.Remove(this);
    }

    private void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 toPlayer =
            (Vector2)player.position - rb.position;

        float distanceToPlayer = toPlayer.magnitude;

        Vector2 chaseDirection = Vector2.zero;

        if (distanceToPlayer > stoppingDistance)
        {
            chaseDirection = toPlayer / distanceToPlayer;
        }

        Vector2 separationDirection =
            CalculateSeparation();

        Vector2 finalDirection =
            chaseDirection +
            separationDirection * separationStrength;

        finalDirection =
            Vector2.ClampMagnitude(finalDirection, 1f);

        rb.linearVelocity =
            finalDirection * moveSpeed;
    }

    private Vector2 CalculateSeparation()
    {
        Vector2 separation = Vector2.zero;

        float radius =
            Mathf.Max(0.01f, separationRadius);

        float radiusSquared = radius * radius;

        for (int i = 0; i < activeEnemies.Count; i++)
        {
            EnemyMovement other = activeEnemies[i];

            if (other == null || other == this)
                continue;

            Vector2 away =
                rb.position - other.rb.position;

            float distanceSquared =
                away.sqrMagnitude;

            if (distanceSquared >= radiusSquared)
                continue;

            // If two enemies overlap exactly,
            // push them in opposite directions.
            if (distanceSquared < 0.0001f)
            {
                away =
                    enemyOrder < other.enemyOrder
                        ? Vector2.right
                        : Vector2.left;
            }

            float distance = away.magnitude;

            float weight =
                1f - Mathf.Clamp01(distance / radius);

            separation += away.normalized * weight;
        }

        return Vector2.ClampMagnitude(separation, 1f);
    }
}
