using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EmptyVialInteraction : MonoBehaviour
{
    public GameObject fillObject; // GPVFX_Bottle_A_Fill
    public DiningDoor diningDoor; 
    public Text interactionText;

    private bool isPlayerNear = false;
    private bool isFilled = false;

    private Inventory inventory;
    private InventorySelector selector;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        selector = FindObjectOfType<InventorySelector>();

        if (fillObject != null)
            fillObject.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && !isFilled && Input.GetKeyDown(KeyCode.F))
        {
            IInventoryItem selectedItem = GetSelectedItem();

            if (selectedItem != null && selectedItem.Name == "BloodVial")
            {
                FillVial();
            }
            else
            {
                interactionText.text = "You cannot fill the vial with this.";
                StartCoroutine(ClearTextAfterSeconds(2f));
            }
        }
    }

    private IInventoryItem GetSelectedItem()
    {
        if (inventory == null || selector == null) return null;

        List<IInventoryItem> items = inventory.GetItems();
        int index = selector.currentSlot;

        if (index >= 0 && index < items.Count)
            return items[index];

        return null;
    }

private void FillVial()
{
    isFilled = true;

    if (fillObject != null)
        fillObject.SetActive(true);

    if (diningDoor != null)
        diningDoor.isLocked = false;

    // Remove the selected item (BloodVial) from inventory
    if (inventory != null && selector != null)
    {
        int index = selector.currentSlot;
        inventory.RemoveItemAtSlot(index);
    }

    interactionText.text = "The vial is filled. Dining door unlocked.";
    StartCoroutine(ClearTextAfterSeconds(3f));
}


    private IEnumerator ClearTextAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (!isPlayerNear || isFilled)
            interactionText.text = "";
        else
            interactionText.text = "Press F to fill the empty vial.";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (!isFilled)
                interactionText.text = "Press F to fill the empty vial.";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (!isFilled)
                interactionText.text = "";
        }
    }
}
