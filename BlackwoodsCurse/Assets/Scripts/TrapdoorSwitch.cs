using UnityEngine;
using System.Collections;

public class TrapdoorSwitch : MonoBehaviour
{
    public HUD hud;
    public Inventory inventory;
    public InventorySelector selector;
    public GameObject trapdoor;      // Trapdoor to open
    public float rotationAngle = -40f;

    public GameObject wallToDestroy;
    public AudioSource wallBreakSound;
    public AudioSource trapdoorOpenSound; // Optional

    private bool playerNear = false;
    private bool isActivated = false;
    private bool wallIsBroken = false;
    private bool messageOverridden = false;

    public void EnableSwitch()  // Call from wall when broken
    {
        wallIsBroken = true;
    }

    void Update()
    {
        if (!playerNear || isActivated || !wallIsBroken) return;

        if (!messageOverridden)
        {
            hud.txt.text = "The switch seems far...";
        }

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
                StartCoroutine(ShowTemporaryMessage("It’s not long enough.", 1.5f));
            }
        }
    }

    void ActivateSwitch()
    {
        isActivated = true;

        // Rotate switch
        transform.localRotation *= Quaternion.Euler(rotationAngle, 0f, 0f);

        // Open trapdoor
        if (trapdoor != null)
        {
            trapdoor.transform.localRotation *= Quaternion.Euler(90f, 0f, 0f);

            if (trapdoorOpenSound != null)
                trapdoorOpenSound.Play();
        }

        // Destroy wall upstairs
        if (wallToDestroy != null)
        {
            Destroy(wallToDestroy);

            if (wallBreakSound != null)
                wallBreakSound.Play();
        }
// Remove key items from inventory
inventory.RemoveItem("Flashlight");
inventory.RemoveItem("Crowbar");
inventory.RemoveItem("Knife");
inventory.RefreshUI(); // Optional: ensure UI updates immediately
        hud.HideMessage();
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
