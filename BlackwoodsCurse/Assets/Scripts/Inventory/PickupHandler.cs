using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    public float pickupRange = 3f;
    public HUD hud;                  // Reference to your HUD script
    public Inventory inventory;     // Reference to Inventory script
    private IInventoryItem currentItem = null;

    void Update()
    {
        if (currentItem != null && Input.GetKeyDown(KeyCode.F))
        {
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
            hud.VisibleMessage();
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
