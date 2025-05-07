using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;


public class CombineHandler : MonoBehaviour
{
    public Inventory inventory;
    public InventorySelector selector;
    public HUD hud;
    public GameObject ropeMagnetPrefab;

    private bool canCombine = false;
    private string nearbyItem = ""; // "Rope" or "Magnet"
    private GameObject nearbyObject = null;

    public List<Outline> vaseOutlines; // Assign 3 outline components in Inspector



    void Update()
    {
        if (canCombine && Input.GetKeyDown(KeyCode.X))
        {
            List<IInventoryItem> items = inventory.GetItems();
            int selectedIndex = selector.currentSlot;

            if (selectedIndex >= items.Count)
            {
                StartCoroutine(ShowTemporaryMessage("Empty Box", 7f));
                return;
            }

            var selectedItem = items[selectedIndex];
            string selectedName = selectedItem.Name;

            if ((selectedName == "Rope" && nearbyItem == "Magnet") ||
                (selectedName == "Magnet" && nearbyItem == "Rope"))
            {
                // Remove both
                int removeIndex = -1;
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Name == nearbyItem)
                        removeIndex = i;
                }

                if (removeIndex != -1)
                    inventory.RemoveItemAtSlot(removeIndex);

                inventory.RemoveItemAtSlot(selectedIndex);

                // Add combined item via Inventory.AddItem()
GameObject combined = Instantiate(ropeMagnetPrefab);
IInventoryItem combinedItem = combined.GetComponent<IInventoryItem>();
inventory.AddItem(combinedItem); // use the AddItem method to avoid direct list access

                foreach (var outline in vaseOutlines)
{
    if (outline != null)
        outline.enabled = true;
}


                if (nearbyObject != null)
                {
                    Destroy(nearbyObject);
                    nearbyObject = null;
                    nearbyItem = "";
canCombine = false;

                }


                StartCoroutine(ShowTemporaryMessage("Combined Successfully!", 2f));
                canCombine = false;
                nearbyItem = "";
            }
            else
            {
                StartCoroutine(ShowTemporaryMessage("Can't Combine", 7f));
            }

            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<IInventoryItem>() as MonoBehaviour;
        if (item != null)
        {
            string name = item.GetComponent<IInventoryItem>().Name;
            List<IInventoryItem> items = inventory.GetItems();

            bool hasRope = items.Exists(i => i.Name == "Rope");
            bool hasMagnet = items.Exists(i => i.Name == "Magnet");

            if ((name == "Rope" && hasMagnet) || (name == "Magnet" && hasRope))
            {
                canCombine = true;
                nearbyItem = name;
                hud.txt.text = "Combine X";
                nearbyObject = other.gameObject;

            }
        }
       

    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInventoryItem>() != null)
        {
            canCombine = false;
            nearbyItem = "";
            nearbyObject = null;
            hud.HideMessage();
        }

    }

    IEnumerator ShowTemporaryMessage(string msg, float duration)
    {
        hud.txt.text = msg;
        yield return new WaitForSeconds(duration);
        hud.HideMessage();
    }

    public void DisableVaseGlow()
{
    foreach (var outline in vaseOutlines)
    {
        if (outline != null)
            outline.enabled = false;
    }
}

}
