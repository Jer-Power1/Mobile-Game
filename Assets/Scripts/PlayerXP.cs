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

    public int Level => level;
    public int CurrentXP => currentXP;
    public int XPToNextLevel => xpToNextLevel;

    public void AddXP(int amount)
    {
        currentXP += amount;

        Debug.Log(
            $"XP: {currentXP}/{xpToNextLevel}"
        );

        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentXP -= xpToNextLevel;

        level++;

        xpToNextLevel += 3;

        Debug.Log(
            $"LEVEL UP! Level {level}"
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