using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pausePanel;

    private bool isPaused;

    public bool IsPaused => isPaused;

    private void Start()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        isPaused = false;
    }

    private void Update()
    {
        // Escape works for testing in the Unity Editor.
        // Android Back is also commonly represented by Escape.
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (isPaused)
            return;

        isPaused = true;

        Time.timeScale = 0f;

        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        if (!isPaused)
            return;

        isPaused = false;

        Time.timeScale = 1f;

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            PauseGame();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            PauseGame();
        }
    }
}