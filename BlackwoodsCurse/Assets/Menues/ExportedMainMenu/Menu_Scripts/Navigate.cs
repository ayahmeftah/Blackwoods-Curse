using System.Diagnostics;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class Navigate : MonoBehaviour
{
    // Load any scene with fade
    public void goToScene(string sceneName)
    {
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadSceneWithFade(sceneName);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }

    // Quit the game
    public void exitGame()
    {
        UnityEngine.Application.Quit();
    }

    // Start a new game from the cutscene scene
    public void StartNewGame()
    {
        goToScene("Cut_Scene1"); // Replace with actual cutscene scene name
    }
}
