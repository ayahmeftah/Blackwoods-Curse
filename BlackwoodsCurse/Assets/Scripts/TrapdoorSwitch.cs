using UnityEngine;
using System.Collections;

public class TrapdoorSwitch : MonoBehaviour
{
    public HUD hud;
    public Inventory inventory;
    public InventorySelector selector;
    public GameObject trapdoor;             // Trapdoor to open
    public Light flashlightLight;           // Flashlight light to disable
    public float rotationAngle = -40f;

    [Header("Audio")]
    public AudioSource trapdoorOpenSound;   // Play this immediately
    public AudioSource wallBreakSound;      // Play this after 4 seconds

    [Header("Wall To Destroy")]
    public GameObject wallToDestroy;        // Assign wall upstairs here

    private bool playerNear = false;
    private bool isActivated = false;
    private bool wallIsBroken = false;
    private bool messageOverridden = false;

    public void EnableSwitch()
    {
        wallIsBroken = true;
    }

    void Update()
    {
        if (!playerNear || isActivated || !wallIsBroken || messageOverridden) return;

        hud.txt.text = "The switch seems far...";

        if (Input.GetKeyDown(KeyCode.F))
        {
            var items = inventory.GetItems();
            int idx = selector.currentSlot;

            if (idx < items.Count && items[idx].Name == "Crowbar")
            {
                ActivateSwitch();
            }
            else
            {
                StartCoroutine(ShowTemporaryMessage("It’s not long enough.", 2f));
            }
        }
    }

    void ActivateSwitch()
    {
        isActivated = true;

        // Rotate switch
        transform.localRotation *= Quaternion.Euler(rotationAngle, 0f, 0f);

        // Rotate trapdoor open
        if (trapdoor != null)
        {
            trapdoor.transform.localRotation *= Quaternion.Euler(90f, 0f, 0f);

            if (trapdoorOpenSound != null)
                trapdoorOpenSound.Play(); // 🔊 Play trapdoor sound now
        }

        // Delay wall break sound & destruction
        if (wallToDestroy != null && wallBreakSound != null)
        {
            StartCoroutine(PlayWallBreakAfterDelay(4f));
        }

        // Remove items
        inventory.RemoveItem("Flashlight");
        inventory.RemoveItem("Crowbar");
        inventory.RemoveItem("Knife");
        inventory.RemoveItem("Hammer");
        inventory.RefreshUI();

        // Disable flashlight light
        if (flashlightLight != null)
            flashlightLight.enabled = false;

        hud.txt.text = "You flipped the switch!";
    }

    IEnumerator PlayWallBreakAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        wallBreakSound.Play();

        // Optional: destroy the wall if needed visually
        Destroy(wallToDestroy);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNear = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            hud.HideMessage();
        }
    }

    IEnumerator ShowTemporaryMessage(string msg, float duration)
    {
        messageOverridden = true;
        hud.txt.text = msg;
        yield return new WaitForSeconds(duration);
        messageOverridden = false;

        if (playerNear && !isActivated)
        {
            hud.txt.text = "The switch seems far...";
        }
    }
}
