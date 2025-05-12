using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public Light flashlightLight;
    public InventorySelector inventorySelector;
    public Inventory inventory;
    public HUD hud;

    // Internal States
    private bool isOn = false;
    private bool hasLens = false;
    private bool isBlue = false;
    private bool hasDisplayedMessage = false;
    private float messageTimer = 0f;

    void Start()
    {
        flashlightLight.enabled = false;
        flashlightLight.color = Color.white;
        hud.HideMessage();
    }

    void Update()
    {
        bool selected = IsFlashlightSelected();

        // Display "Press T to turn flashlight on" only once, and keep it for 3 seconds
        if (selected && !hasDisplayedMessage)
        {
            hud.txt.text = "Press T to turn flashlight on";
            messageTimer = 3f;
            hasDisplayedMessage = true;
        }

        // Countdown the message timer
        if (messageTimer > 0)
        {
            messageTimer -= Time.deltaTime;
            if (messageTimer <= 0)
            {
                hud.HideMessage();
            }
        }

        // === Handle flashlight toggling ===
        if (selected && Input.GetKeyDown(KeyCode.T))
        {
            isOn = !isOn;
            flashlightLight.enabled = isOn;

            // If the flashlight is turned on and has a lens, show the R prompt
            if (isOn && hasLens)
            {
                hud.txt.text = "Press R to use the lens";
                messageTimer = 3f;
            }

            if (!isOn)
            {
                hud.HideMessage();
            }
        }

        // === Handle blue light toggling ===
        if (hasLens && isOn && Input.GetKeyDown(KeyCode.R))
        {
            isBlue = !isBlue;
            flashlightLight.color = isBlue ? Color.cyan : Color.white;

            hud.txt.text = isBlue
                ? "Press R to deactivate blue light"
                : "Press R to activate blue light";

            messageTimer = 3f;
        }

        // === Ensure the border remains blue if the lens is merged ===
        if (hasLens)
        {
            SetFlashlightSlotToBlue();
        }
    }

    public bool IsFlashlightSelected()
    {
        var items = inventory.GetItems();
        int idx = inventorySelector.currentSlot;
        return idx >= 0 && idx < items.Count && items[idx].Name == "Flashlight";
    }

    public void MergeLens()
    {
        hasLens = true;
        SetFlashlightSlotToBlue();
        hud.txt.text = "Lens merged successfully!";
        messageTimer = 3f;
    }

    // === Method to make sure the slot stays blue ===
    private void SetFlashlightSlotToBlue()
    {
        var items = inventory.GetItems();
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Name == "Flashlight")
            {
                inventorySelector.slotBorders[i].color = Color.blue;
                break;
            }
        }
    }
}
