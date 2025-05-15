using UnityEngine;
using TMPro;
using System.Collections;

public class LevelManager : MonoBehaviour
{   public TextMeshProUGUI levelCompleteText;
    public CanvasGroup levelCompleteCanvasGroup;

     public static LevelManager Instance;
    public TextMeshProUGUI levelText;

    private int currentLevel = 1;
    public bool isBonusLevel = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AdvanceLevel()
    {
        currentLevel++;
        DisplayMessage();
        UpdateLevelText();
    }

    public void UpdateLevelText()
    {
        if (levelText == null) return;

        if (isBonusLevel)
            levelText.text = "Secret Level";
        else
            levelText.text = "Level " + currentLevel;
    }

    public void EnterBonusLevel()
    {
        isBonusLevel = true;
        UpdateLevelText();
    }

    public void ExitBonusLevel()
    {
        isBonusLevel = false;
        UpdateLevelText();
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
    public void DisplayMessage()
    {
        StartCoroutine(DisplayLevelCompleteMessage());
    }

    private IEnumerator DisplayLevelCompleteMessage()
    {
        levelCompleteText.text = "Level Complete!";
        levelCompleteCanvasGroup.alpha = 1;
        yield return new WaitForSeconds(3f); // Display for 3 seconds
        levelCompleteCanvasGroup.alpha = 0;
    }

}
