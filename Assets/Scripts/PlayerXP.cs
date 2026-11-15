
using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [Header("Level")]
    [SerializeField] private int level = 1;

    [Header("XP")]
    [SerializeField] private int currentXP = 0;
    [SerializeField] private int xpToNextLevel = 5;

    [Header("UI")]
    [SerializeField] private LevelUpUI levelUpUI;

    private int totalXPCollected;

    public int Level => level;
    public int CurrentXP => currentXP;
    public int XPToNextLevel => xpToNextLevel;
    public int TotalXPCollected => totalXPCollected;

    private void Awake()
    {
        xpToNextLevel = Mathf.Max(1, xpToNextLevel);
    }

    public void AddXP(int amount)
    {
        if (amount <= 0)
            return;

        currentXP += amount;
        totalXPCollected += amount;

        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        int xpCost = xpToNextLevel;

        // Spend the XP needed for this level.
        // Any extra XP is carried forward.
        currentXP -= xpCost;

        level++;

        // Increase the next level requirement.
        xpToNextLevel += 3;

        Debug.Log(
            $"LEVEL UP! Level {level}"
        );

        // CA2 local telemetry.
        // Records the XP spent to reach the level.
        Telemetry.Log(
            "level_up",
            "level=" + level +
            " xp_spent=" + xpCost +
            " xp_remaining=" + currentXP +
            " next_xp_cost=" + xpToNextLevel +
            " total_xp_collected=" + totalXPCollected
        );

        if (levelUpUI != null)
        {
            levelUpUI.ShowLevelUp();
        }
        else
        {
            Debug.LogError(
                "PlayerXP: LevelUpUI has not been assigned!"
            );
        }
    }
}
