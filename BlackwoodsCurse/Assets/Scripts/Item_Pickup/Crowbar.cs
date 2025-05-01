using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowbar : MonoBehaviour, IInventoryItem
{
    public string itemName = "Crowbar";
    public Sprite itemIcon;

    public bool canBePickedUp = false; // only becomes true after water drains

    public string Name => itemName;
    public Sprite Image => itemIcon;

    public void OnPickup()
    {
        gameObject.SetActive(false);
    }
}
