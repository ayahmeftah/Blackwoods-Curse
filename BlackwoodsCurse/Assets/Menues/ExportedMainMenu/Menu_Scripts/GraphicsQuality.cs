using TMPro;
using UnityEngine;

public class GraphicsQuality : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;

    void Start()
    {
        // Default to "High" (index 3)
        if (PlayerPrefs.HasKey("GraphicsQuality"))
        {
            int savedLevel = PlayerPrefs.GetInt("GraphicsQuality");
            QualitySettings.SetQualityLevel(savedLevel);
            graphicsDropdown.value = savedLevel;
        }
        else
        {
            int defaultLevel = 3; // "High"
            QualitySettings.SetQualityLevel(defaultLevel);
            graphicsDropdown.value = defaultLevel;
            PlayerPrefs.SetInt("GraphicsQuality", defaultLevel);
            PlayerPrefs.Save();
        }

        graphicsDropdown.RefreshShownValue();
        graphicsDropdown.onValueChanged.AddListener(ChangeGraphicsQuality);
    }

    public void ChangeGraphicsQuality(int level)
    {
        QualitySettings.SetQualityLevel(level);
        PlayerPrefs.SetInt("GraphicsQuality", level);
        PlayerPrefs.Save();
    }
}
