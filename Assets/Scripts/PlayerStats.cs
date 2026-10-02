using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] private float baseMoveSpeed = 5f;

    [Header("Passive Levels")]
    [SerializeField] private int mightLevel = 0;
    [SerializeField] private int hasteLevel = 0;
    [SerializeField] private int moveSpeedLevel = 0;

    public const int MaxPassiveLevel = 8;

    // +10% damage per Might level
    public float DamageMultiplier =>
        1f + (mightLevel * 0.10f);

    // +8% attack speed per Haste level
    public float AttackSpeedMultiplier =>
        1f + (hasteLevel * 0.08f);

    // +5% movement speed per level
    public float MoveSpeedMultiplier =>
        1f + (moveSpeedLevel * 0.05f);

    public float MoveSpeed =>
        baseMoveSpeed * MoveSpeedMultiplier;

    public int MightLevel => mightLevel;
    public int HasteLevel => hasteLevel;
    public int MoveSpeedLevel => moveSpeedLevel;

    public bool UpgradeMight()
    {
        if (mightLevel >= MaxPassiveLevel)
            return false;

        mightLevel++;
        return true;
    }

    public bool UpgradeHaste()
    {
        if (hasteLevel >= MaxPassiveLevel)
            return false;

        hasteLevel++;
        return true;
    }

    public bool UpgradeMoveSpeed()
    {
        if (moveSpeedLevel >= MaxPassiveLevel)
            return false;

        moveSpeedLevel++;
        return true;
    }
}