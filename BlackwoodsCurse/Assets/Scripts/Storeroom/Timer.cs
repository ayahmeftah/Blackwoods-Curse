using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime = 90;
    [SerializeField] CanvasGroup timerCanvasGroup;

    private bool isTimerRunning = false;

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
        else if (remainingTime <= 0)
        {
            remainingTime = 0;
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
}
