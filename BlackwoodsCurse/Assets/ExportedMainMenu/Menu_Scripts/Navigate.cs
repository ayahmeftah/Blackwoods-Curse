using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigate : MonoBehaviour
{
    // Load a specific scene by name
    public void goToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Quit the game
    public void exitGame()
    {
        Application.Quit();
    }

    // Called by "New Game" button — starts from cutscene
    public void StartNewGame()
    {
        SceneManager.LoadScene("Cut_Scene1");
}
}
