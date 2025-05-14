using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime = 12;
    [SerializeField] CanvasGroup timerCanvasGroup;

    private bool isTimerRunning = false;
    private bool isGameOver = false;

    // Game Over UI Elements
    [SerializeField] CanvasGroup gameOverCanvasGroup;
    [SerializeField] TextMeshProUGUI gameOverText;

    void Start()
    {
        timerCanvasGroup.alpha = 0;
    }

    void Update()
    {
        if (isTimerRunning && remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime <= 0 && !isGameOver)
        {
            remainingTime = 0;
            isGameOver = true;
            GameOver();
        }

        if (remainingTime < 11)
        {
            timerText.color = Color.red;
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer()
    {
        StartCoroutine(StartTimerWithDelay());
    }

    // Coroutine to handle the delay
    private IEnumerator StartTimerWithDelay()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        timerCanvasGroup.alpha = 1;          // Make it visible
        isTimerRunning = true;               // Start the timer
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        timerText.color = Color.green;
        StartCoroutine(HideTimerAfterDelay());
    }

    private IEnumerator HideTimerAfterDelay()
    {
        yield return new WaitForSeconds(5f); // Wait 5 seconds
        timerCanvasGroup.alpha = 0f;         // Hide the timer UI
    }


    // Game Over logic
    private void GameOver()
    {
        Debug.Log("Time's Up! Game Over.");

        // Disable player controls or interactions if needed
        StartCoroutine(FadeInGameOverScreen());
    }

    // Fade-in effect for Game Over screen
    private IEnumerator FadeInGameOverScreen()
    {
        float duration = 2f; // 2 seconds fade-in
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            gameOverCanvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }

        gameOverCanvasGroup.alpha = 1;
    }

    public void ReduceTime(float seconds)
    {
        remainingTime = Mathf.Max(0, remainingTime - seconds);
    }

    public void HighlightTimer(Color color)
    {
        timerText.color = color;
    }
}
