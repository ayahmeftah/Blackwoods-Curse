using System.Collections;

using UnityEngine;

using UnityEngine.SceneManagement;



public class PauseMenuManager : MonoBehaviour

{

    public GameObject instructionsPanel; // Assign: InstructionsPanel 

    public GameObject pauseButtons; // Assign: PauseButtons 



    private bool isPaused = false;



    void Update()

    {

        if (Input.GetKeyDown(KeyCode.Escape))

        {

            if (instructionsPanel.activeSelf)

            {

                HideInstructions();

            }

            else if (isPaused)

            {

                Resume();

            }

            else

            {

                Pause();

            }

        }

    }



    public void ShowInstructions()

    {

        pauseButtons.SetActive(false); // Hide pause buttons only 

        instructionsPanel.SetActive(true); // Show instructions 



        Time.timeScale = 0f;

        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.None;

    }



    public void HideInstructions()

    {

        instructionsPanel.SetActive(false);

        pauseButtons.SetActive(true);



        Time.timeScale = 0f;

        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.None;

    }



    public void Resume()

    {

        pauseButtons.SetActive(false);

        instructionsPanel.SetActive(false);



        Time.timeScale = 1f;

        isPaused = false;



        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;

    }



    public void Pause()

    {

        pauseButtons.SetActive(true);

        instructionsPanel.SetActive(false);



        Time.timeScale = 0f;

        isPaused = true;



        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.None;

    }



    public void LoadMainMenu()

    {

        Time.timeScale = 1f;

        SceneManager.LoadScene("StartMenuScene");

    }

}