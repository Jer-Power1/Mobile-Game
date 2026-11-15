
using UnityEngine;
using UnityEngine.UI;

public class PauseSettingsMenu : MonoBehaviour
{
    [Header("Settings Panel")]
    [SerializeField] private GameObject settingsPanel;

    [Header("Toggles")]
    [SerializeField] private Toggle hapticsToggle;
    [SerializeField] private Toggle fpsToggle;

    [Header("Performance Overlay")]
    [SerializeField] private PerformanceOverlay performanceOverlay;

    private void Awake()
    {
        if (hapticsToggle != null)
        {
            hapticsToggle.onValueChanged.AddListener(
                OnHapticsChanged
            );
        }

        if (fpsToggle != null)
        {
            fpsToggle.onValueChanged.AddListener(
                OnFPSChanged
            );
        }
    }

    private void OnEnable()
    {
        // Always return to the main Pause screen.
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        RefreshToggles();
    }

    private void OnDestroy()
    {
        if (hapticsToggle != null)
        {
            hapticsToggle.onValueChanged.RemoveListener(
                OnHapticsChanged
            );
        }

        if (fpsToggle != null)
        {
            fpsToggle.onValueChanged.RemoveListener(
                OnFPSChanged
            );
        }
    }

    private void RefreshToggles()
    {
        if (hapticsToggle != null)
        {
            hapticsToggle.SetIsOnWithoutNotify(
                GameSettings.HapticsEnabled
            );
        }

        if (fpsToggle != null)
        {
            fpsToggle.SetIsOnWithoutNotify(
                GameSettings.ShowFPS
            );
        }
    }

    public void OpenSettings()
    {
        RefreshToggles();

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
        else
        {
            Debug.LogError(
                "PauseSettingsMenu: SettingsPanel " +
                "is not assigned."
            );
        }
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    private void OnHapticsChanged(bool enabled)
    {
        GameSettings.SetHaptics(enabled);
    }

    private void OnFPSChanged(bool enabled)
    {
        GameSettings.SetShowFPS(enabled);

        if (performanceOverlay != null)
        {
            performanceOverlay.SetVisible(enabled);
        }
        else
        {
            Debug.LogWarning(
                "PauseSettingsMenu: PerformanceOverlay " +
                "is not assigned."
            );
        }
    }

    public void TestHaptics()
    {
        GameSettings.Vibrate();
    }
}
