
using UnityEngine;

public static class GameSettings
{
    private const string HapticsKey =
        "ArcaneSurvivor_HapticsEnabled";

    private const string FPSKey =
        "ArcaneSurvivor_ShowFPS";

    // Both settings are enabled by default.
    public static bool HapticsEnabled =>
        PlayerPrefs.GetInt(HapticsKey, 1) == 1;

    public static bool ShowFPS =>
        PlayerPrefs.GetInt(FPSKey, 1) == 1;

    public static void SetHaptics(bool enabled)
    {
        PlayerPrefs.SetInt(
            HapticsKey,
            enabled ? 1 : 0
        );

        PlayerPrefs.Save();

        Debug.Log(
            "[Settings] Haptics: " +
            (enabled ? "ON" : "OFF")
        );
    }

    public static void SetShowFPS(bool enabled)
    {
        PlayerPrefs.SetInt(
            FPSKey,
            enabled ? 1 : 0
        );

        PlayerPrefs.Save();

        Debug.Log(
            "[Settings] FPS display: " +
            (enabled ? "ON" : "OFF")
        );
    }

    public static void Vibrate()
    {
        if (!HapticsEnabled)
            return;

#if UNITY_ANDROID && !UNITY_EDITOR
        Handheld.Vibrate();
#else
        Debug.Log(
            "[Haptics] Vibration requested. " +
            "Physical vibration is tested on Android."
        );
#endif
    }
}
