using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoringScript : MonoBehaviour
{ 
    public void ShowMainMenu()
    {
        SceneManager.LoadScene("StartMenuScene");
    }
    
}
