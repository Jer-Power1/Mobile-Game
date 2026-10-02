using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int baseDamage = 1;
    [SerializeField] private float baseAttackInterval = 1f;
    [SerializeField] private float attackRange = 8f;

    private PlayerStats stats;
    private float timer;

    // Magic Bolt starts as the player's first weapon at Level 1
    private int weaponLevel = 1;

    public int Level => weaponLevel;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        float currentInterval =
            baseAttackInterval / stats.AttackSpeedMultiplier;

        if (timer >= currentInterval)
        {
            Attack();
            timer = 0f;
        }
    }

    public void UpgradeWeapon()
    {
        if (weaponLevel >= 8)
            return;

        weaponLevel++;

        Debug.Log($"Magic Bolt upgraded to Level {weaponLevel}");
    }

    private void Attack()
    {
        GameObject target = FindClosestEnemy();

        if (target == null)
            return;

        Vector2 direction =
            (target.transform.position - transform.position).normalized;

        GameObject projectile = Instantiate(
            projectilePrefab,
            transform.position,
            Quaternion.identity
        );

        // Magic Bolt gains +1 base damage per weapon level
        int weaponDamage =
            baseDamage + (weaponLevel - 1);

        // Might passive affects Magic Bolt damage
        int finalDamage = Mathf.Max(
            1,
            Mathf.RoundToInt(
                weaponDamage * stats.DamageMultiplier
            )
        );

        Projectile projectileScript =
            projectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            projectileScript.Setup(direction, finalDamage);
        }
    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closest = null;
        float closestDistance = attackRange;

        foreach (GameObject enemy in enemies)
        {
            float distance =
                Vector2.Distance(
                    transform.position,
                    enemy.transform.position
                );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }
}