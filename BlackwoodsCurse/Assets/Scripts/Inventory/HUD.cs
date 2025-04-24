using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [Header("References")]
    public Text txt;                     // The pickup prompt text
    public Transform inventoryPanel;     // The parent object of slots (slot0, slot1, etc.)
    public Inventory inventory;          // Reference to your Inventory script
    public InventorySelector selector;   // <-- This is the missing piece!

    void Start()
    {
        inventory.ItemAdded += OnItemAdded;
        txt.text = "";
    }

    private void OnItemAdded(object sender, InventoryEventArgs e)
    {
        if (e.SlotIndex == -1)
        {
            txt.text = "Inventory Full!";
            Invoke("HideMessage", 2f); // hide after 2 seconds
            return;
        }

        Transform slot = inventoryPanel.GetChild(e.SlotIndex);
        Image itemImage = slot.Find("border/ItemImage")?.GetComponent<Image>();

        if (itemImage != null)
        {
            itemImage.enabled = true;
            itemImage.sprite = e.Item.Image;
        }
    }


    public void VisibleMessage()
    {
        txt.text = "Pickup F";
    }

    public void HideMessage()
    {
        txt.text = "";
    }
}
