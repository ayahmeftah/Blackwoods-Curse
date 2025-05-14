using UnityEngine;
using System.Collections;


public class ChestLock : MonoBehaviour
{
    public HUD hud;
    public Inventory inventory;
    public InventorySelector selector;
    public ChestLid chestLid;  // reference to lid script
    
    private bool playerNear = false;
    private bool isBroken = false;
    private bool messageOverridden = false;


    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // prevent falling at start
    }

    void Update()
{
    if (!playerNear || isBroken || messageOverridden) return;

    hud.txt.text = "The lock seems rusty...";

    if (Input.GetKeyDown(KeyCode.F))
    {
        var items = inventory.GetItems();
        int idx = selector.currentSlot;

        if (idx < items.Count && items[idx].Name == "Knife")
        {
            BreakLock();
        }
        else
        {
            StartCoroutine(ShowTemporaryMessage("Only something sharp can break this", 2f));
        }
    }
}

    void BreakLock()
    {
        isBroken = true;
        rb.isKinematic = false;     // enable fall
        rb.useGravity = true;
        chestLid.UnlockChest();     // notify lid it can be opened
        
        // hud.txt.text = "You broke the lock.";
        // Destroy(gameObject, 2f);    // remove lock after it drops
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

IEnumerator ShowTemporaryMessage(string message, float duration)
{
    messageOverridden = true;
    hud.txt.text = message;
    yield return new WaitForSeconds(duration);
    messageOverridden = false;

    // Only show the default message if still near and not broken
    if (playerNear && !isBroken)
    {
        hud.txt.text = "The lock seems rusty...";
    }


}
}
