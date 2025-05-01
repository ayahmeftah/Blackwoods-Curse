using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BathtubDrainTrigger : MonoBehaviour
{
    public HUD hud;
    public Inventory inventory;
    public InventorySelector selector;

    public string requiredItemName = "Knife";

    public Renderer waterRenderer;
    public float fadeDuration = 1.5f;

    public AudioSource drainSound;
    public GameObject crowbar;

    private bool isDrained = false;
    private bool isPlayerNear = false;
    private bool isShowingTempMessage = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isDrained)
        {
            isPlayerNear = true;
            UpdateMessage();
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
        // Update the on-screen message live when player is near
        if (isPlayerNear && !isDrained && !isShowingTempMessage)
        {
            UpdateMessage();
        }

        // Handle 'F' key press
        if (isPlayerNear && !isDrained && Input.GetKeyDown(KeyCode.F))
        {
            if (IsHoldingRequiredItem())
            {
                BreakDrain();
            }
            else
            {
                // Show "You need something sharp..." temporarily
                StopAllCoroutines();
                StartCoroutine(ShowTempMessage("You need something sharp...", 1.5f));
            }
        }
    }

    void UpdateMessage()
    {
        if (isDrained || isShowingTempMessage) return;

        if (IsHoldingRequiredItem())
        {
            hud.txt.text = "Press F to break the drain";
        }
        else
        {
            hud.txt.text = "Something's clogging the drain...";
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

        if (drainSound != null)
            drainSound.Play();

        if (waterRenderer != null)
            StartCoroutine(FadeOutWater());

        Invoke(nameof(RevealCrowbar), fadeDuration);
    }

    private System.Collections.IEnumerator FadeOutWater()
    {
        Material mat = waterRenderer.material;
        Color originalColor = mat.color;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            mat.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        mat.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    void RevealCrowbar()
    {
        if (crowbar != null)
        {
            var crowbarScript = crowbar.GetComponent<Crowbar>();
            if (crowbarScript != null)
                crowbarScript.canBePickedUp = true;
        }
    }

    private System.Collections.IEnumerator ShowTempMessage(string message, float duration)
    {
        isShowingTempMessage = true;
        hud.txt.text = message;

        yield return new WaitForSeconds(duration);

        isShowingTempMessage = false;
        UpdateMessage();
    }
}
