using UnityEngine;

public class Lens : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public FlashlightController flashlightController;
    public HUD hud;

    // Internal State
    private bool playerNear = false;
    private float messageTimer = 0f;
    private bool isShowingErrorMessage = false;

    void Update()
    {
        // Handle pressing X to merge
        if (playerNear && Input.GetKeyDown(KeyCode.X))
        {
            TryMerge();
        }

        // Handle message countdown
        if (messageTimer > 0)
        {
            messageTimer -= Time.deltaTime;
            if (messageTimer <= 0 && isShowingErrorMessage)
            {
                isShowingErrorMessage = false;
                hud.txt.text = "Press X to merge lens.";
            }
            else if (messageTimer <= 0)
            {
                hud.HideMessage();
            }
        }
    }

    private void TryMerge()
    {
        // Check if the flashlight is currently selected in the inventory
        bool flashlightSelected = flashlightController.IsFlashlightSelected();

        if (!flashlightSelected)
        {
            // Show error message if wrong item is held
            hud.txt.text = "Unable to merge – wrong item";
            messageTimer = 3f; // Stay for 3 seconds
            isShowingErrorMessage = true;
            return;
        }

        // === Merge into flashlight ===
        flashlightController.MergeLens();

        // Display success message
        hud.txt.text = "Lens merged successfully!";
        messageTimer = 3f;

        // Destroy the lens object in the world
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;

            // Only show the merge message if there is no error
            if (!isShowingErrorMessage)
            {
                hud.txt.text = "Press X to merge lens.";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            hud.HideMessage();
        }
    }
}
