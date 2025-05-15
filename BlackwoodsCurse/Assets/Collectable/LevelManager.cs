using UnityEngine;
using TMPro;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public TMP_Text levelText;
    private int currentLevel = 1;
    public TextMeshProUGUI levelCompleteText;
    public CanvasGroup levelCompleteCanvasGroup;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateLevelDisplay();
    }

    public void AdvanceLevel()
    {
        currentLevel++;
        DisplayMessage();
        UpdateLevelDisplay();
    }

    private void UpdateLevelDisplay()
    {
        if (levelText != null)
            levelText.text = "Level " + currentLevel;
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
