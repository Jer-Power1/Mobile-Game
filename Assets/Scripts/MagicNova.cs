using UnityEngine;

public class MagicNova : MonoBehaviour
{
    [Header("Weapon Stats")]
    [Header("Visual")]
    [SerializeField] private GameObject novaEffectPrefab;
    [SerializeField] private float baseCooldown = 3f;
    [SerializeField] private float baseRadius = 3f;
    [SerializeField] private int baseDamage = 2;

    private PlayerStats playerStats;

    private float timer;
    private bool unlocked = false;
    private int weaponLevel = 0;

    public int Level => weaponLevel;
    public bool Unlocked => unlocked;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (!unlocked)
            return;

        timer += Time.deltaTime;

        float cooldown =
            baseCooldown / playerStats.AttackSpeedMultiplier;

        if (timer >= cooldown)
        {
            FireNova();
            timer = 0f;
        }
    }

    public void UpgradeWeapon()
    {
        if (weaponLevel >= 8)
            return;

        weaponLevel++;

        if (!unlocked)
        {
            unlocked = true;
            timer = baseCooldown;
        }
    }

    private void FireNova()
    {
        float radius = GetCurrentRadius();
        GameObject effect = Instantiate(
    novaEffectPrefab,
    transform.position,
    Quaternion.identity
);

        effect.GetComponent<NovaEffect>().Setup(radius);
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            radius
        );

        int damage = GetCurrentDamage();

        foreach (Collider2D hit in hits)
        {
            if (!hit.CompareTag("Enemy"))
                continue;

            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        Debug.Log(
            $"Magic Nova fired - Damage: {damage}, Radius: {radius}"
        );
    }

    private int GetCurrentDamage()
    {
        float weaponDamage =
            baseDamage + (weaponLevel - 1);

        return Mathf.Max(
            1,
            Mathf.RoundToInt(
                weaponDamage * playerStats.DamageMultiplier
            )
        );
    }

    private float GetCurrentRadius()
    {
        return baseRadius + ((weaponLevel - 1) * 0.15f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(
            transform.position,
            GetCurrentRadius()
        );
    }
}