using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowbar : MonoBehaviour, IInventoryItem
{
    public string itemName = "Crowbar";
    public Sprite itemIcon;

    public bool canBePickedUp = false; // Only becomes true after water drains

    public string Name => itemName;
    public Sprite Image => itemIcon;

    public void OnPickup()
    {
        if (!canBePickedUp) // 🚩 Prevents pickup if not ready
        {
            Debug.Log("Crowbar is not ready to be picked up.");
            return;
        }
        
        Debug.Log("Picked up the crowbar!");
        gameObject.SetActive(false); // It disappears from the world
    }
}


