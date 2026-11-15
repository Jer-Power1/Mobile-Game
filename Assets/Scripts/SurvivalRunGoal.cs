
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

    private float elapsedTime;
    private bool runFinished;
    private int lastDisplayedSeconds = -1;

    private void Start()
    {
        Time.timeScale = 1f;

        elapsedTime = 0f;
        runFinished = false;

        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
        else
        {
            Debug.LogError(
                "SurvivalRunGoal: Win Panel is not assigned!"
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

        // The existing death screen takes priority.
        if (gameOverPanel != null &&
            gameOverPanel.activeInHierarchy)
        {
            runFinished = true;

            Debug.Log(
                "[telemetry] level_fail level_id=arena_01 " +
                "time_s=" + elapsedTime.ToString("F1") +
                " cause=game_over"
            );

            return;
        }

        // Pauses and level-up screens freeze the timer.
        if (Time.timeScale <= 0f)
            return;

        elapsedTime += Time.deltaTime;

        UpdateObjectiveText();

        if (elapsedTime >= survivalDuration)
        {
            WinGame();
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

        int totalSeconds = Mathf.CeilToInt(remaining);

        // Update text only when seconds change.
        if (totalSeconds == lastDisplayedSeconds)
            return;

        lastDisplayedSeconds = totalSeconds;

        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        objectiveText.text =
            $"SURVIVE  {minutes:00}:{seconds:00}";
    }

    private void WinGame()
    {
        if (runFinished)
            return;

        runFinished = true;

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
            "[telemetry] level_complete level_id=arena_01 " +
            "time_s=" + elapsedTime.ToString("F1")
        );
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
