using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowbar : MonoBehaviour, IInventoryItem
{
    public string itemName = "Crowbar";
    public Sprite itemIcon;

    public bool canBePickedUp = false;

    public string Name => itemName;
    public Sprite Image => itemIcon;

    public void OnPickup()
    {
        Debug.Log("Picked up the crowbar!");
        gameObject.SetActive(false);
    }
}
