using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParkourFallDetection : MonoBehaviour
{
    public int playerLives = 3;
    public Transform respawnPoint;
    public float detectionDelay = 1.5f;

    private bool isDetectionActive = false;

    // private void OnTriggerEnter(Collider other)
    // {
  
    // }

 public void HandleFall(Transform player)
{
    if (!isDetectionActive) return;

    // Only count as fall if player is below certain height
    if (player.position.y >= 5.5f) return; // Adjust this based on your safe platform's Y level

    playerLives--;

    if (playerLives <= 0)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    else
    {
        player.position = respawnPoint.position;
        Debug.Log("Remaining lives: " + playerLives);
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
    }
}

