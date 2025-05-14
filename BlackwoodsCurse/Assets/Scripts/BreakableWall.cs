using UnityEngine;
using System.Collections;

public class BreakableWall : MonoBehaviour
{
    public HUD hud;
    public Inventory inventory;
    public InventorySelector selector;
    public TrapdoorSwitch hiddenSwitch;
    //public GameObject brokenVersion; // optional broken wall prefab
    public AudioSource breakSound;

    private bool playerNear = false;
    private bool isBroken = false;
    private bool messageOverridden = false;

    void Update()
    {
        if (!playerNear || isBroken || messageOverridden) return;

        hud.txt.text = "This wall looks weak...";

        if (Input.GetKeyDown(KeyCode.F))
        {
            var items = inventory.GetItems();
            int idx = selector.currentSlot;

            if (idx < items.Count && items[idx].Name == "Hammer")
            {
                BreakWall();
            }
            else
            {
                StartCoroutine(ShowTemporaryMessage("You need something strong to break this.", 2f));
            }
        }
    }

    void BreakWall()
    {
        isBroken = true;

        if (breakSound != null)
            breakSound.Play();

        hud.txt.text = "Wall smashed!";

        // if (brokenVersion != null)
        // {
        //     Instantiate(brokenVersion, transform.position, transform.rotation);
        // }
        hiddenSwitch.EnableSwitch();
        Destroy(gameObject, 0.2f);
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

        if (playerNear && !isBroken)
        {
            hud.txt.text = "This wall looks weak...";
        }
    }
}
