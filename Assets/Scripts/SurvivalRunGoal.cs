
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SurvivalRunGoal : MonoBehaviour
{
    [Header("Run Settings")]
    [SerializeField] private float survivalDuration = 180f;
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    [Header("UI References")]
    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Optional Statistics UI")]
    [SerializeField] private TMP_Text winStatsText;
    [SerializeField] private TMP_Text gameOverStatsText;

    private const string BestTimeKey =
        "ArcaneSurvivor_BestTime";

    private const string TotalRunsKey =
        "ArcaneSurvivor_TotalRuns";

    private const string TotalVictoriesKey =
        "ArcaneSurvivor_TotalVictories";

    private const string LastResultKey =
        "ArcaneSurvivor_LastResult";

    private float elapsedTime;
    private float bestSurvivalTime;

    private int totalRuns;
    private int totalVictories;
    private int lastDisplayedSeconds = -1;

    private bool runFinished;

    private void Start()
    {
        Time.timeScale = 1f;

        LoadProgress();

        elapsedTime = 0f;
        runFinished = false;

        totalRuns++;

        PlayerPrefs.SetInt(TotalRunsKey, totalRuns);
        PlayerPrefs.Save();

        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
        else
        {
            Debug.LogError(
                "SurvivalRunGoal: WinPanel not assigned!"
            );
        }

        UpdateObjectiveText();

        Debug.Log(
            "[telemetry] level_start level_id=arena_01"
        );
    }

    private void Update()
    {
        if (runFinished)
            return;

        // Existing Game Over screen takes priority.
        if (gameOverPanel != null &&
            gameOverPanel.activeInHierarchy)
        {
            FinishRun(false);
            return;
        }

        // Stop the timer during pause and level-up.
        if (Time.timeScale <= 0f)
            return;

        elapsedTime += Time.deltaTime;

        UpdateObjectiveText();

        if (elapsedTime >= survivalDuration)
        {
            FinishRun(true);
        }
    }

    private void FinishRun(bool won)
    {
        if (runFinished)
            return;

        runFinished = true;

        if (won)
        {
            elapsedTime = survivalDuration;
            totalVictories++;

            if (objectiveText != null)
            {
                objectiveText.text = "SURVIVED!";
            }

            Time.timeScale = 0f;

            if (winPanel != null)
            {
                winPanel.SetActive(true);
            }

            Debug.Log(
                "[telemetry] level_complete " +
                "level_id=arena_01 " +
                "time_s=" + elapsedTime.ToString("F1")
            );
        }
        else
        {
            Debug.Log(
                "[telemetry] level_fail " +
                "level_id=arena_01 " +
                "time_s=" + elapsedTime.ToString("F1") +
                " cause=game_over"
            );
        }

        SaveRunResult(won);
        RefreshStatisticsText();
    }

    private void LoadProgress()
    {
        bestSurvivalTime =
            PlayerPrefs.GetFloat(BestTimeKey, 0f);

        totalRuns =
            PlayerPrefs.GetInt(TotalRunsKey, 0);

        totalVictories =
            PlayerPrefs.GetInt(TotalVictoriesKey, 0);

        string lastResult =
            PlayerPrefs.GetString(LastResultKey, "None");

        Debug.Log(
            "[Save] Loaded: " +
            "Best=" + bestSurvivalTime.ToString("F1") +
            " Runs=" + totalRuns +
            " Victories=" + totalVictories +
            " Last=" + lastResult
        );
    }

    private void SaveRunResult(bool won)
    {
        UpdateBestTime();

        PlayerPrefs.SetInt(
            TotalVictoriesKey,
            totalVictories
        );

        PlayerPrefs.SetString(
            LastResultKey,
            won ? "Victory" : "Defeat"
        );

        PlayerPrefs.Save();

        Debug.Log(
            "[Save] Run result saved successfully."
        );
    }

    private void UpdateBestTime()
    {
        if (elapsedTime > bestSurvivalTime)
        {
            bestSurvivalTime = elapsedTime;

            PlayerPrefs.SetFloat(
                BestTimeKey,
                bestSurvivalTime
            );
        }
    }

    private void SaveCheckpoint()
    {
        // Preserve the best time reached so far,
        // even if Android closes the application.
        if (!runFinished)
        {
            UpdateBestTime();
        }

        PlayerPrefs.Save();
    }

    private void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            SaveCheckpoint();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveCheckpoint();
        }
    }

    private void OnApplicationQuit()
    {
        SaveCheckpoint();
    }

    private void RefreshStatisticsText()
    {
        int seconds =
            Mathf.FloorToInt(bestSurvivalTime);

        int minutes = seconds / 60;
        int remainingSeconds = seconds % 60;

        string statistics =
            $"Best Time: {minutes:00}:{remainingSeconds:00}\n" +
            $"Runs Played: {totalRuns}\n" +
            $"Victories: {totalVictories}";

        if (winStatsText != null)
        {
            winStatsText.text = statistics;
        }

        if (gameOverStatsText != null)
        {
            gameOverStatsText.text = statistics;
        }
    }

    private void UpdateObjectiveText()
    {
        if (objectiveText == null)
            return;

        float remaining = Mathf.Max(
            0f,
            survivalDuration - elapsedTime
        );

        int totalSeconds =
            Mathf.CeilToInt(remaining);

        if (totalSeconds == lastDisplayedSeconds)
            return;

        lastDisplayedSeconds = totalSeconds;

        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        objectiveText.text =
            $"SURVIVE  {minutes:00}:{seconds:00}";
    }

    public void RestartRun()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }

    public void ReturnToMenu()
    {
        if (!Application.CanStreamedLevelBeLoaded(
            mainMenuSceneName))
        {
            Debug.LogError(
                "Menu scene not found: " +
                mainMenuSceneName
            );

            return;
        }

        Time.timeScale = 1f;

        SceneManager.LoadScene(mainMenuSceneName);
    }
}
