using UnityEngine;

public class ArcaneOrbit : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private GameObject orbitProjectilePrefab;

    [Header("Weapon Stats")]
    [SerializeField] private float orbitRadius = 2f;
    [SerializeField] private float rotationSpeed = 120f;
    [SerializeField] private int baseDamage = 1;

    private PlayerStats playerStats;
    private GameObject orbitProjectile;

    private float angle;

    // Starts locked
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
        if (!unlocked || orbitProjectile == null)
            return;

        angle += rotationSpeed * Time.deltaTime;

        float radians = angle * Mathf.Deg2Rad;

        Vector2 offset = new Vector2(
            Mathf.Cos(radians),
            Mathf.Sin(radians)
        ) * orbitRadius;

        orbitProjectile.transform.position =
            (Vector2)transform.position + offset;
    }

    public void UpgradeWeapon()
    {
        if (weaponLevel >= 8)
            return;

        weaponLevel++;

        // First upgrade unlocks the weapon
        if (!unlocked)
        {
            unlocked = true;
            CreateOrb();
        }

        UpdateDamage();

        Debug.Log($"Arcane Orbit upgraded to Level {weaponLevel}");
    }

    private void CreateOrb()
    {
        if (orbitProjectilePrefab == null)
        {
            Debug.LogError(
                "Arcane Orbit: Orbit Projectile Prefab is not assigned!"
            );

            return;
        }

        Vector2 startPosition =
            (Vector2)transform.position + Vector2.right * orbitRadius;

        orbitProjectile = Instantiate(
            orbitProjectilePrefab,
            startPosition,
            Quaternion.identity
        );

        UpdateDamage();
    }

    private void UpdateDamage()
    {
        if (orbitProjectile == null)
            return;

        int weaponDamage =
            baseDamage + (weaponLevel - 1);

        int finalDamage = Mathf.Max(
            1,
            Mathf.RoundToInt(
                weaponDamage * playerStats.DamageMultiplier
            )
        );

        OrbitProjectile projectile =
            orbitProjectile.GetComponent<OrbitProjectile>();

        if (projectile != null)
        {
            projectile.SetDamage(finalDamage);
        }
    }
}