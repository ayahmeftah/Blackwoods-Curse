using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorLock : MonoBehaviour

{
    public string requiredKeyName = "Key"; // Name of the item needed to unlock
    public Text txt;                       // UI prompt
    public InventorySelector selector;
    public HUD hud;
    public AudioSource doorOpenSound;


    private bool isPlayerNear = false;
    private bool isUnlocked = false;
    private bool isOpened = false;

    private Quaternion defaultRot;
    private Quaternion openRot;

    public float smooth = 2.0f;
    public float DoorOpenAngle = 90.0f;
    public float rotationTolerance = 1.0f;

    private Inventory inventory; // Reference to the player’s inventory

    void Start()
    {
        defaultRot = transform.rotation;
        openRot = Quaternion.Euler(defaultRot.eulerAngles + Vector3.up * DoorOpenAngle);

        // Find the player's inventory script
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            if (isUnlocked)
            {
                OpenDoor();
            }
            else if (PlayerIsHoldingKey(requiredKeyName))
            {
                isUnlocked = true;
                txt.text = "";
                RemoveHeldKey(); // ← remove the currently held key
                OpenDoor();
            }
            else
            {
                txt.text = "wrong key";
            }
        }

        // Rotate door toward open state if it's supposed to be open
        if (isOpened && Quaternion.Angle(transform.rotation, openRot) > rotationTolerance)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRot, Time.deltaTime * smooth);
        }
    }

    private void OpenDoor()
    {
        isOpened = true;
        txt.text = "";
        if (doorOpenSound != null)
            doorOpenSound.Play();

    }

    private bool PlayerIsHoldingKey(string keyName)
    {
        int selectedSlot = selector.currentSlot;

        var items = inventory.GetItems();
        if (selectedSlot >= 0 && selectedSlot < items.Count)
        {
            return items[selectedSlot].Name == keyName;
        }

        return false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;

            if (isUnlocked)
                txt.text = "";
            else
                txt.text = "Unlock 'F'";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            txt.text = "";
        }
    }

    private void RemoveHeldKey()
    {
        int selectedSlot = selector.currentSlot;

        var items = inventory.GetItems();
        if (selectedSlot >= 0 && selectedSlot < items.Count)
        {
            items.RemoveAt(selectedSlot);

            // Clear the UI icon for the slot
            Transform slot = hud.inventoryPanel.GetChild(selectedSlot);
            Image itemImage = slot.Find("border/ItemImage")?.GetComponent<Image>();

            if (itemImage != null)
            {
                itemImage.sprite = null;
                itemImage.enabled = false;
            }
        }
    }



}
