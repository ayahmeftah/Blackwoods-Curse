using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDetectionHandler : MonoBehaviour
{
    public int playerLives = 3;
    public Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerLives--;
            Debug.Log("Remaining lives: " + playerLives);

            if (playerLives <= 0)
            {
                //Restart the scene if lives are finished
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                //Respawn the player at the starting safe platform
                other.transform.position = respawnPoint.position;
                
            }
        }
    }
}

