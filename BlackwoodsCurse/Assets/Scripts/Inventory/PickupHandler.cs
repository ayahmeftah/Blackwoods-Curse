using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    public HUD hud;
    public Inventory inventory;
    public InventorySelector selector;

    private IInventoryItem currentItem = null;

    void Update()
    {
        // If the player presses F and is near an item
        if (currentItem != null && Input.GetKeyDown(KeyCode.F))
        {
            List<IInventoryItem> items = inventory.GetItems();

            bool hasRope = items.Exists(i => i.Name == "Rope");
            bool hasMagnet = items.Exists(i => i.Name == "Magnet");

            // 🛠️ Crowbar check: make sure it's pickable before adding
            if (currentItem is Crowbar crowbar && !crowbar.canBePickedUp)
            {
                Debug.Log("❌ Crowbar is not ready to be picked up.");
                hud.txt.text = "You can't pick this up yet.";
                return;
            }

            // 🔄 Rope & Magnet combination logic
            if ((currentItem.Name == "Rope" && hasMagnet) || 
                (currentItem.Name == "Magnet" && hasRope))
            {
                // Block pickup — force combine instead
                return;
            }

            // ✅ Pickup normally
            inventory.AddItem(currentItem);
            hud.HideMessage();
            currentItem.OnPickup(); // Calls OnPickup() on the item to deactivate it
            currentItem = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<IInventoryItem>() as MonoBehaviour;

        if (item != null)
        {
            currentItem = item.GetComponent<IInventoryItem>();
            string itemName = currentItem.Name;

            List<IInventoryItem> items = inventory.GetItems();
            bool hasRope = items.Exists(i => i.Name == "Rope");
            bool hasMagnet = items.Exists(i => i.Name == "Magnet");

            int selectedIndex = selector.currentSlot;

            // 🛠️ Crowbar logic: block pickup if not yet ready
            if (currentItem is Crowbar crowbar && !crowbar.canBePickedUp)
            {
                hud.txt.text = "You can't pick this up yet.";
                Debug.Log("❌ Crowbar is not yet pickable.");
                return;
            }

            // 🔄 Rope & Magnet logic
            if ((itemName == "Rope" && hasMagnet) || (itemName == "Magnet" && hasRope))
            {
                if (selectedIndex >= items.Count)
                {
                    hud.txt.text = "Empty Box";
                }
                else
                {
                    IInventoryItem selectedItem = items[selectedIndex];
                    if ((itemName == "Rope" && selectedItem.Name == "Magnet") ||
                        (itemName == "Magnet" && selectedItem.Name == "Rope"))
                    {
                        hud.txt.text = "Combine X";
                    }
                    else
                    {
                        hud.txt.text = "Can't Combine";
                    }
                }
            }
            else
            {
                hud.txt.text = "Pickup F";
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInventoryItem>() != null)
        {
            currentItem = null;
            hud.HideMessage();
        }
    }

    public void ForceRefreshPickupMessage(GameObject item)
{
    var inventoryItem = item.GetComponent<IInventoryItem>();

    if (inventoryItem != null)
    {
        currentItem = inventoryItem;

        // If it's the crowbar and it's now pickable, show the message
        if (inventoryItem is Crowbar crowbar && crowbar.canBePickedUp)
        {
            hud.txt.text = "Pickup F";
            Debug.Log("🪓 Crowbar is now ready to be picked up!");
        }
    }
}

}
