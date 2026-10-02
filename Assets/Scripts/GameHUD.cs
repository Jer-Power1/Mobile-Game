using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerXP playerXP;

    [Header("Bars")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider xpBar;

    [Header("Text")]
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text timerText;

    private float runTime;

    private void Start()
    {
        SetupHealthBar();
        SetupXPBar();
        UpdateHUD();
    }

    private void Update()
    {
        runTime += Time.deltaTime;

        UpdateHUD();
        UpdateTimer();
    }

    private void SetupHealthBar()
    {
        if (playerHealth == null || healthBar == null)
            return;

        healthBar.minValue = 0;
        healthBar.maxValue = playerHealth.MaxHealth;
        healthBar.value = playerHealth.CurrentHealth;
    }

    private void SetupXPBar()
    {
        if (playerXP == null || xpBar == null)
            return;

        xpBar.minValue = 0;
        xpBar.maxValue = playerXP.XPToNextLevel;
        xpBar.value = playerXP.CurrentXP;
    }

    private void UpdateHUD()
    {
        if (playerHealth != null && healthBar != null)
        {
            healthBar.maxValue = playerHealth.MaxHealth;
            healthBar.value = playerHealth.CurrentHealth;
        }

        if (playerXP != null && xpBar != null)
        {
            xpBar.maxValue = playerXP.XPToNextLevel;
            xpBar.value = playerXP.CurrentXP;
        }

        if (playerXP != null && levelText != null)
        {
            levelText.text = $"Level {playerXP.Level}";
        }
    }

    private void UpdateTimer()
    {
        if (timerText == null)
            return;

        int minutes = Mathf.FloorToInt(runTime / 60f);
        int seconds = Mathf.FloorToInt(runTime % 60f);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}