using UnityEngine;
using TMPro;

public class ScoringMenuUI : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI watchText;

    void Start()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        // Show completed level
        if (LevelManager.Instance != null)
        {
            int currentLevel = LevelManager.Instance.GetCurrentLevel();
            bool isBonus = LevelManager.Instance.isBonusLevel;

            levelText.text = isBonus ? "Secret Level" : "Level " + currentLevel;
        }

        // Show collected watches
        if (CollectibleManager.Instance != null)
        {
            int currentScore = CollectibleManager.Instance.GetCollectedScore();
            watchText.text = $"{currentScore}/10";
        }
    }
}
