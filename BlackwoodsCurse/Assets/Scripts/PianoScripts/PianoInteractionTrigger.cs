using UnityEngine;

public class PianoInteractionTrigger : MonoBehaviour
{
    public HUD hud;
    public GameObject piano3DModel;
    public GameObject pianoOverlay; // Canvas with “Back (X)”
    public Camera mainCamera;
    public Camera pianoCamera;
    public GameObject playerController;

    public GameObject inventoryUI;


    private bool isPlayerNear = false;
    private bool inPianoMode = false;

    void Start()
{
    mainCamera.enabled = true;
    pianoCamera.enabled = false;

    pianoOverlay.SetActive(false);
    piano3DModel.SetActive(false);
}

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

    public void ExitPianoMode()
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

        // ✅ Only show the message if puzzle is not solved
        if (!PianoPuzzleManager.Instance.IsPuzzleSolved)
            hud.txt.text = "Enter Piano Mode P";
        else
            hud.HideMessage();
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


    public bool InPianoMode => inPianoMode;

public void ForceExit()
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

    if (hud != null)
        hud.HideMessage();
}


}

