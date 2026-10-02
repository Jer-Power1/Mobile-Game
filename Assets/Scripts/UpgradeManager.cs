using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public enum UpgradeType
    {
        Weapon,
        Passive
    }

    [System.Serializable]
    public class Upgrade
    {
        public string upgradeName;
        public UpgradeType type;
        public int level;
        public int maxLevel = 8;
        public bool owned;
    }

    [Header("Player")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private AutoAttack magicBoltWeapon;
    [SerializeField] private ArcaneOrbit arcaneOrbitWeapon;
    [SerializeField] private MagicNova magicNovaWeapon;

    [Header("Weapons")]
    public Upgrade magicBolt = new Upgrade
    {
        upgradeName = "Magic Bolt",
        type = UpgradeType.Weapon,
        level = 1,
        maxLevel = 8,
        owned = true
    };

    public Upgrade arcaneOrbit = new Upgrade
    {
        upgradeName = "Arcane Orbit",
        type = UpgradeType.Weapon,
        level = 0,
        maxLevel = 8,
        owned = false
    };

    public Upgrade magicNova = new Upgrade
    {
        upgradeName = "Magic Nova",
        type = UpgradeType.Weapon,
        level = 0,
        maxLevel = 8,
        owned = false
    };

    [Header("Passives")]
    public Upgrade might = new Upgrade
    {
        upgradeName = "Might",
        type = UpgradeType.Passive,
        level = 0,
        maxLevel = 8,
        owned = false
    };

    public Upgrade haste = new Upgrade
    {
        upgradeName = "Haste",
        type = UpgradeType.Passive,
        level = 0,
        maxLevel = 8,
        owned = false
    };

    public Upgrade swiftBoots = new Upgrade
    {
        upgradeName = "Swift Boots",
        type = UpgradeType.Passive,
        level = 0,
        maxLevel = 8,
        owned = false
    };

    private const int MaxWeapons = 3;
    private const int MaxPassives = 3;

    private void Awake()
    {
        if (playerStats != null)
        {
            if (magicBoltWeapon == null)
            {
                magicBoltWeapon =
                    playerStats.GetComponent<AutoAttack>();
            }

            if (arcaneOrbitWeapon == null)
            {
                arcaneOrbitWeapon =
                    playerStats.GetComponent<ArcaneOrbit>();
            }

            if (magicNovaWeapon == null)
            {
                magicNovaWeapon =
                    playerStats.GetComponent<MagicNova>();
            }
        }

        CheckReferences();
    }

    private void CheckReferences()
    {
        if (playerStats == null)
        {
            Debug.LogError(
                "UpgradeManager: PlayerStats is not assigned!"
            );
        }

        if (magicBoltWeapon == null)
        {
            Debug.LogError(
                "UpgradeManager: AutoAttack is not assigned!"
            );
        }

        if (arcaneOrbitWeapon == null)
        {
            Debug.LogError(
                "UpgradeManager: ArcaneOrbit is not assigned!"
            );
        }

        if (magicNovaWeapon == null)
        {
            Debug.LogError(
                "UpgradeManager: MagicNova is not assigned!"
            );
        }
    }

    public List<Upgrade> GetUpgradeChoices()
    {
        List<Upgrade> available =
            GetAvailableUpgrades();

        List<Upgrade> choices =
            new List<Upgrade>();

        while (
            choices.Count < 3 &&
            available.Count > 0)
        {
            int randomIndex =
                Random.Range(
                    0,
                    available.Count
                );

            choices.Add(
                available[randomIndex]
            );

            available.RemoveAt(
                randomIndex
            );
        }

        return choices;
    }

    private List<Upgrade> GetAvailableUpgrades()
    {
        List<Upgrade> available =
            new List<Upgrade>();

        Upgrade[] allUpgrades =
        {
            magicBolt,
            arcaneOrbit,
            magicNova,
            might,
            haste,
            swiftBoots
        };

        int weaponCount =
            CountOwnedWeapons();

        int passiveCount =
            CountOwnedPassives();

        foreach (Upgrade upgrade in allUpgrades)
        {
            if (
                upgrade.owned &&
                upgrade.level >= upgrade.maxLevel)
            {
                continue;
            }

            if (
                !upgrade.owned &&
                upgrade.type == UpgradeType.Weapon &&
                weaponCount >= MaxWeapons)
            {
                continue;
            }

            if (
                !upgrade.owned &&
                upgrade.type == UpgradeType.Passive &&
                passiveCount >= MaxPassives)
            {
                continue;
            }

            available.Add(upgrade);
        }

        return available;
    }

    public void SelectUpgrade(Upgrade upgrade)
    {
        if (upgrade == null)
            return;

        if (
            upgrade.owned &&
            upgrade.level >= upgrade.maxLevel)
        {
            return;
        }

        if (!upgrade.owned)
        {
            upgrade.owned = true;
            upgrade.level = 1;
        }
        else
        {
            upgrade.level++;
        }

        ApplyUpgrade(upgrade);

        Debug.Log(
            $"{upgrade.upgradeName} upgraded " +
            $"to Level {upgrade.level}"
        );
    }

    private void ApplyUpgrade(Upgrade upgrade)
    {
        if (upgrade == might)
        {
            if (playerStats != null)
            {
                playerStats.UpgradeMight();
            }
        }
        else if (upgrade == haste)
        {
            if (playerStats != null)
            {
                playerStats.UpgradeHaste();
            }
        }
        else if (upgrade == swiftBoots)
        {
            if (playerStats != null)
            {
                playerStats.UpgradeMoveSpeed();
            }
        }
        else if (upgrade == magicBolt)
        {
            if (magicBoltWeapon != null)
            {
                magicBoltWeapon.UpgradeWeapon();
            }
        }
        else if (upgrade == arcaneOrbit)
        {
            if (arcaneOrbitWeapon != null)
            {
                arcaneOrbitWeapon.UpgradeWeapon();
            }
        }
        else if (upgrade == magicNova)
        {
            if (magicNovaWeapon != null)
            {
                magicNovaWeapon.UpgradeWeapon();
            }
        }
    }

    private int CountOwnedWeapons()
    {
        int count = 0;

        if (magicBolt.owned)
            count++;

        if (arcaneOrbit.owned)
            count++;

        if (magicNova.owned)
            count++;

        return count;
    }

    private int CountOwnedPassives()
    {
        int count = 0;

        if (might.owned)
            count++;

        if (haste.owned)
            count++;

        if (swiftBoots.owned)
            count++;

        return count;
    }
}