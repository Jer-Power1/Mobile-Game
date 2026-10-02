using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject levelUpPanel;
    [SerializeField] private UpgradeManager upgradeManager;

    [Header("Choice Buttons")]
    [SerializeField] private Button choice1Button;
    [SerializeField] private Button choice2Button;
    [SerializeField] private Button choice3Button;

    [Header("Choice Text")]
    [SerializeField] private TMP_Text choice1Text;
    [SerializeField] private TMP_Text choice2Text;
    [SerializeField] private TMP_Text choice3Text;

    private List<UpgradeManager.Upgrade> currentChoices =
        new List<UpgradeManager.Upgrade>();

    private void Start()
    {
        levelUpPanel.SetActive(false);

        choice1Button.onClick.AddListener(
            () => SelectChoice(0)
        );

        choice2Button.onClick.AddListener(
            () => SelectChoice(1)
        );

        choice3Button.onClick.AddListener(
            () => SelectChoice(2)
        );
    }

    public void ShowLevelUp()
    {
        currentChoices = upgradeManager.GetUpgradeChoices();

        if (currentChoices.Count == 0)
        {
            Debug.Log("No upgrades available.");
            return;
        }

        Time.timeScale = 0f;

        levelUpPanel.SetActive(true);

        SetupButton(
            0,
            choice1Button,
            choice1Text
        );

        SetupButton(
            1,
            choice2Button,
            choice2Text
        );

        SetupButton(
            2,
            choice3Button,
            choice3Text
        );
    }

    private void SetupButton(
        int index,
        Button button,
        TMP_Text buttonText)
    {
        if (index >= currentChoices.Count)
        {
            button.gameObject.SetActive(false);
            return;
        }

        button.gameObject.SetActive(true);

        UpgradeManager.Upgrade upgrade =
            currentChoices[index];

        buttonText.text = GetUpgradeText(upgrade);
    }

    private string GetUpgradeText(
        UpgradeManager.Upgrade upgrade)
    {
        string status;

        if (!upgrade.owned)
        {
            if (upgrade.type ==
                UpgradeManager.UpgradeType.Weapon)
            {
                status = "NEW WEAPON";
            }
            else
            {
                status = "NEW PASSIVE";
            }
        }
        else
        {
            status =
                $"Level {upgrade.level} → {upgrade.level + 1}";
        }

        string description =
            GetDescription(upgrade);

        return
            $"{upgrade.upgradeName}\n\n" +
            $"{status}\n\n" +
            $"{description}";
    }

    private string GetDescription(
        UpgradeManager.Upgrade upgrade)
    {
        if (upgrade == upgradeManager.magicBolt)
        {
            return "Fires at the nearest enemy.\nIncreases damage.";
        }

        if (upgrade == upgradeManager.arcaneOrbit)
        {
            return "A magical orb circles you.\nIncreases damage.";
        }

        if (upgrade == upgradeManager.magicNova)
        {
            return "Damages nearby enemies.\nIncreases damage and radius.";
        }

        if (upgrade == upgradeManager.might)
        {
            return "+10% damage to all weapons.";
        }

        if (upgrade == upgradeManager.haste)
        {
            return "+8% attack speed.";
        }

        if (upgrade == upgradeManager.swiftBoots)
        {
            return "+5% movement speed.";
        }

        return "";
    }

    private void SelectChoice(int index)
    {
        if (index < 0 ||
            index >= currentChoices.Count)
        {
            return;
        }

        UpgradeManager.Upgrade selectedUpgrade =
            currentChoices[index];

        upgradeManager.SelectUpgrade(
            selectedUpgrade
        );

        CloseLevelUp();
    }

    private void CloseLevelUp()
    {
        levelUpPanel.SetActive(false);

        Time.timeScale = 1f;

        currentChoices.Clear();
    }
}