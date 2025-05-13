using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlashlightController : MonoBehaviour
{
    [Header("References")]
    public Light flashlightLight;
    public InventorySelector inventorySelector;
    public Inventory inventory;
    public HUD hud;

    private bool isOn = false;
    private bool hasLens = false;
    private bool isBlue = false;

    private int lastSlot = -1;
    private bool tMessageShown = false;
    private Coroutine rPromptRoutine;

    void Start()
    {
        flashlightLight.enabled = false;
        hud.HideMessage();
    }

    void Update()
    {
        int idx = inventorySelector.currentSlot;
        List<IInventoryItem> items = inventory.GetItems();

        bool selected = idx >= 0 && idx < items.Count && items[idx].Name == "Flashlight";

        if (selected && idx != lastSlot && !tMessageShown)
            StartCoroutine(ShowTMessage());

        lastSlot = idx;

        if (selected)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                isOn = !isOn;
                flashlightLight.enabled = isOn;

                if (hasLens && isOn)
                {
                    ShowRPrompt();
                }
                else
                {
                    hud.HideMessage();
                }
            }

            if (hasLens && isOn && Input.GetKeyDown(KeyCode.R))
            {
                isBlue = !isBlue;
                flashlightLight.color = isBlue ? Color.cyan : Color.white;
                ShowRPrompt();
            }
        }
    }

    IEnumerator ShowTMessage()
    {
        tMessageShown = true;
        hud.txt.text = "Press T to turn on/off the light";
        yield return new WaitForSeconds(1.5f);
        hud.HideMessage();
        tMessageShown = false;
    }

    void ShowRPrompt()
    {
        if (rPromptRoutine != null)
            StopCoroutine(rPromptRoutine);
        rPromptRoutine = StartCoroutine(HideRPromptAfterDelay());
    }

    IEnumerator HideRPromptAfterDelay()
    {
        hud.txt.text = isBlue
            ? "Press R to deactivate blue light."
            : "Press R to activate blue light.";
        yield return new WaitForSeconds(1.5f);
        hud.HideMessage();
    }

    public void MergeLens()
    {
        hasLens = true;

        int slot = inventorySelector.currentSlot;
        if (slot >= 0 && slot < inventorySelector.slotBorders.Length)
            inventorySelector.slotBorders[slot].color = Color.blue;

        ShowRPrompt(); // Show after merging
    }
    public bool IsFlashlightSelected()
{
    var items = inventory.GetItems();
    int idx = inventorySelector.currentSlot;
    return idx >= 0 && idx < items.Count && items[idx].Name == "Flashlight";
}

}
