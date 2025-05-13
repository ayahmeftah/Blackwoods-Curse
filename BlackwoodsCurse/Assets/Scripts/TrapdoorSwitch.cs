using UnityEngine;
using System.Collections;

public class TrapdoorSwitch : MonoBehaviour
{
    public HUD hud;
    public Inventory inventory;
    public InventorySelector selector;
    public GameObject trapdoor;      // Assign the trapdoor to open
    public float rotationAngle = -40f;
    public AudioSource creakSound;

    private bool playerNear = false;
    private bool isActivated = false;
    private bool wallIsBroken = false;
    private bool messageOverridden = false;
    public GameObject wallToDestroy;
public AudioSource wallBreakSound;


    public void EnableSwitch()  // Call this from BreakableWall when it breaks
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

        // Rotate switch lever
        transform.localRotation *= Quaternion.Euler(rotationAngle, 0f, 0f);

        if (trapdoor != null)
        {
            // Rotate the trapdoor 90 degrees on the X-axis
            trapdoor.transform.localRotation *= Quaternion.Euler(90f, 0f, 0f);
        }

        //Trapdoor Open
        creakSound?.Play();

        if (wallToDestroy != null)
        {
            Destroy(wallToDestroy);

            if (wallBreakSound != null)
            wallBreakSound.Play();
        }

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
