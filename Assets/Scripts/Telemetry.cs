
using System;
using System.IO;
using UnityEngine;

public class Telemetry : MonoBehaviour
{
    [Header("Telemetry Settings")]
    [SerializeField] private bool saveToFile = true;

    private static bool sessionStarted;
    private static string sessionId;

    private string logPath;

    [RuntimeInitializeOnLoadMethod(
        RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetSession()
    {
        sessionStarted = false;
        sessionId = null;
    }

    private void Awake()
    {
        logPath = Path.Combine(
            Application.persistentDataPath,
            "telemetry.log"
        );
    }

    private void OnEnable()
    {
        if (saveToFile)
        {
            Application.logMessageReceived += HandleLog;
        }

        if (!sessionStarted)
        {
            sessionStarted = true;

            sessionId = Guid.NewGuid()
                .ToString("N")
                .Substring(0, 8);

            Log(
                "session_start",
                "session_id=" + sessionId +
                " app_version=" + Application.version +
                " device_model=" +
                SystemInfo.deviceModel.Replace(" ", "_") +
                " operating_system=" +
                SystemInfo.operatingSystem.Replace(" ", "_")
            );
        }
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(
        string message,
        string stackTrace,
        LogType type)
    {
        if (!message.StartsWith(
            "[telemetry] ",
            StringComparison.Ordinal))
        {
            return;
        }

        try
        {
            File.AppendAllText(
                logPath,
                DateTime.UtcNow.ToString("O") +
                " " + message + Environment.NewLine
            );
        }
        catch (Exception exception)
        {
            Debug.LogWarning(
                "Could not save telemetry: " +
                exception.Message
            );
        }
    }

    public static void Log(
        string eventName,
        string parameters = "")
    {
        if (string.IsNullOrEmpty(parameters))
        {
            Debug.Log("[telemetry] " + eventName);
        }
        else
        {
            Debug.Log(
                "[telemetry] " + eventName +
                " " + parameters
            );
        }
    }
}
