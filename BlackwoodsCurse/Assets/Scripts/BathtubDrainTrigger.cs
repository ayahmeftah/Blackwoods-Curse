using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BathtubDrainTrigger : MonoBehaviour
{
    public HUD hud;
    public Inventory inventory;
    public InventorySelector selector;

    public string requiredItemName = "Knife";

    public Animator waterAnimator;       // Optional: plays water draining
    public GameObject crowbar;           // The object to reveal
    public AudioSource drainSound;       // Optional: sound effect

    private bool isDrained = false;
    private bool isPlayerNear = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isDrained)
        {
            isPlayerNear = true;

            if (IsHoldingRequiredItem())
            {
                hud.txt.text = "Break F";
            }
            else
            {
                hud.txt.text = "Something's clogging the drain...";
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            hud.HideMessage();
        }
    }

    void Update()
    {
        if (isPlayerNear && !isDrained && Input.GetKeyDown(KeyCode.F))
        {
            if (IsHoldingRequiredItem())
            {
                BreakDrain();
            }
            else
            {
                hud.txt.text = "You need something sharp...";
            }
        }
    }

    bool IsHoldingRequiredItem()
    {
        int slot = selector.currentSlot;
        var items = inventory.GetItems();
        return (slot >= 0 && slot < items.Count && items[slot].Name == requiredItemName);
    }

    void BreakDrain()
    {
        isDrained = true;
        hud.HideMessage();

        // Play animation or drain effect
        if (waterAnimator != null)
            waterAnimator.SetTrigger("Drain");

        if (drainSound != null)
            drainSound.Play();

        // Reveal crowbar after 1.5 seconds
        Invoke(nameof(RevealCrowbar), 1.5f);
    }

    void RevealCrowbar()
    {
        if (crowbar != null)
            crowbar.SetActive(true);
    }
}
