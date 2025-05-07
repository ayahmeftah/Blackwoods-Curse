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
        if (currentItem != null && Input.GetKeyDown(KeyCode.F))
        {
            List<IInventoryItem> items = inventory.GetItems();

            bool hasRope = items.Exists(i => i.Name == "Rope");
            bool hasMagnet = items.Exists(i => i.Name == "Magnet");

            // Only allow pickup if player does NOT already have the other item
            if ((currentItem.Name == "Rope" && hasMagnet) || 
                (currentItem.Name == "Magnet" && hasRope))
            {
                // Block pickup — force combine instead
                return;
            }

            // Pickup normally
            inventory.AddItem(currentItem);
            hud.HideMessage();
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

            if ((itemName == "Rope" && hasMagnet) || (itemName == "Magnet" && hasRope))
            {
                // Can't pick up, only combine
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
}
