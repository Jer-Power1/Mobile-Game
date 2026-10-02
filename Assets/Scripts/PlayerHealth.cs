using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;

    [Header("Damage Protection")]
    [SerializeField] private float invincibilityTime = 0.5f;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;

    private float invincibilityTimer;
    private bool isDead;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsDead => isDead;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (invincibilityTimer > 0f)
        {
            invincibilityTimer -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        if (invincibilityTimer > 0f)
            return;

        currentHealth -= damage;

        currentHealth = Mathf.Clamp(
            currentHealth,
            0,
            maxHealth
        );

        invincibilityTimer = invincibilityTime;

        Debug.Log(
            $"Player HP: {currentHealth}/{maxHealth}"
        );

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (isDead)
            return;

        currentHealth += amount;

        currentHealth = Mathf.Clamp(
            currentHealth,
            0,
            maxHealth
        );
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;

        Debug.Log("PLAYER DIED");

        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            Debug.LogError(
                "PlayerHealth: Game Over Panel is not assigned!"
            );
        }
    }
}