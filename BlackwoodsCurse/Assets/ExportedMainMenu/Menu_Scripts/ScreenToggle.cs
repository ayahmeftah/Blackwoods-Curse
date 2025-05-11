using UnityEngine;
using UnityEngine.UI;

public class ScreenToggle : MonoBehaviour
{
    public Toggle FullScreenToggle;
    private bool isFullScreen;

    void Start()
    {
        // Load saved preference or default to true
        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            isFullScreen = PlayerPrefs.GetInt("Fullscreen") == 1;
        }
        else
        {
            isFullScreen = true; // default
            PlayerPrefs.SetInt("Fullscreen", 1);
            PlayerPrefs.Save();
        }

        Screen.fullScreen = isFullScreen;
        FullScreenToggle.isOn = isFullScreen;

        // Add listener
        FullScreenToggle.onValueChanged.AddListener(ChangeFullScreen);
    }

    public void ChangeFullScreen(bool isOn)
    {
        isFullScreen = isOn;
        Screen.fullScreen = isFullScreen;

        PlayerPrefs.SetInt("Fullscreen", isFullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }
}
