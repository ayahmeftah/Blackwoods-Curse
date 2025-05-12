using UnityEngine;

public class PianoInteractionTrigger : MonoBehaviour
{
    public HUD hud;
    public GameObject piano3DModel;
    public GameObject pianoOverlay; // Canvas with “Back (B)”
    public Camera mainCamera;
    public Camera pianoCamera;
    public GameObject playerController;

    public GameObject inventoryUI;


    private bool isPlayerNear = false;
    private bool inPianoMode = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.P) && !inPianoMode)
        {
            EnterPianoMode();
        }

        if (inPianoMode && Input.GetKeyDown(KeyCode.X))
        {
            ExitPianoMode();
        }
    }

    void EnterPianoMode()
    {
        if (inventoryUI != null)
            inventoryUI.SetActive(false);


        inPianoMode = true;
        hud.HideMessage();

        mainCamera.enabled = false;
        pianoCamera.enabled = true;

        piano3DModel.SetActive(true);
        pianoOverlay.SetActive(true);
        if (playerController != null)
            playerController.SetActive(false);
    }

    void ExitPianoMode()
    {
        if (inventoryUI != null)
            inventoryUI.SetActive(true);


        inPianoMode = false;

        mainCamera.enabled = true;
        pianoCamera.enabled = false;

        piano3DModel.SetActive(false);
        pianoOverlay.SetActive(false);
        if (playerController != null)
            playerController.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            hud.txt.text = "Enter Piano Mode P";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            hud.HideMessage();
        }
    }
}

