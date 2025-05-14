using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ParkourFallDetection : MonoBehaviour
{
    public int playerLives = 4;
    public Transform respawnPoint;
    public float detectionDelay = 1.5f;
    public Text livesText;
    public float fadeDuration = 2f;
    private bool isDetectionActive = false;
    public AudioSource audioSource;
    public AudioClip lifeLostClip;
    public AudioClip deathClip;
    private Coroutine fadeRoutine;

    public void HandleFall(Transform player)
    {
        if (!isDetectionActive) return;

        if (player.position.y >= 5.5f) return;

        playerLives--;

        if (playerLives <= 0)
        {
            if (livesText != null)
            {
                livesText.text = "You lost! Restarting...";
                livesText.color = Color.red;
                livesText.CrossFadeAlpha(1f, 0f, true);
                StartCoroutine(FadeOutText());
            }

            // Play game over sound
            if (audioSource != null && deathClip != null)
                audioSource.PlayOneShot(deathClip);

            StartCoroutine(RestartAfterDelay(3f));
        }
        else
        {
            // Play life lost sound
            if (audioSource != null && lifeLostClip != null)
                audioSource.PlayOneShot(lifeLostClip);

            player.position = respawnPoint.position;
            UpdateLivesUI();
        }

    }

    public void ActivateDetectionWithDelay()
    {
        StartCoroutine(EnableDetectionAfterDelay());
    }

    private IEnumerator EnableDetectionAfterDelay()
    {
        yield return new WaitForSeconds(detectionDelay);
        isDetectionActive = true;
        Debug.Log("Fall detection activated.");
        // UpdateLivesUI();

    }

    private IEnumerator RestartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateLivesUI()
    {
    if (livesText != null)
    {
        livesText.text = $"Lives Remaining: {playerLives}";
        livesText.color = Color.white;
        livesText.CrossFadeAlpha(1f, 0f, true);
        StartFadeText(); 
    }
    }
    private IEnumerator FadeOutText()
    {
        yield return new WaitForSeconds(3f);
        livesText.CrossFadeAlpha(0f, fadeDuration, false);
    }

    private void StartFadeText()
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeOutText());
    }
}
