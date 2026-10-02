using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowCredits()
    {
        Debug.Log("Game created by Jerry Power");
    }
}