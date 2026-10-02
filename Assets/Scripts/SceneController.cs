using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quit Game");
    }
}