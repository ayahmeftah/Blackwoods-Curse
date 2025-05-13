using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 8;
    // List to hold all the current inventory items
    private List<IInventoryItem> mItems = new List<IInventoryItem>();

    // Event triggered when a new item is added to the inventory
    public event EventHandler<InventoryEventArgs> ItemAdded;

    // Method to add an item to the inventory
   public void AddItem(IInventoryItem item)
{
    if (item == null || (item as MonoBehaviour) == null)
        return; // prevent adding destroyed items

    if (mItems.Count < SLOTS)
    {
        Collider collider = (item as MonoBehaviour).GetComponent<Collider>();

        if (collider == null || collider.enabled)
        {
            if (collider != null)
                collider.enabled = false;

            mItems.Add(item);
            item.OnPickup();

            if (ItemAdded != null)
            {
                ItemAdded(this, new InventoryEventArgs(item, mItems.Count - 1));
            }
        }
    }
    else
    {
        if (ItemAdded != null)
        {
            ItemAdded(this, new InventoryEventArgs(null, -1)); // signal inventory full
        }
    }
}

    public List<IInventoryItem> GetItems()
    {
        return mItems;
    }

    public void RemoveItemAtSlot(int index)
    {
        if (index >= 0 && index < mItems.Count)
        {
            mItems.RemoveAt(index);

            // Notify HUD to refresh UI
            HUD hud = FindObjectOfType<HUD>();
            if (hud != null)
            {
                hud.RefreshInventoryUI(mItems);
            }
        }
    }

    public void RefreshUI()
    {
    HUD hud = FindObjectOfType<HUD>();
    if (hud != null)
    {
        hud.RefreshInventoryUI(mItems);
    }
    }

    public void RemoveItem(string itemName)
{
    IInventoryItem itemToRemove = mItems.Find(item => item.Name == itemName);
    if (itemToRemove != null)
    {
        mItems.Remove(itemToRemove);
        Debug.Log($"Removed {itemName} from Inventory.");
    }
}


}
