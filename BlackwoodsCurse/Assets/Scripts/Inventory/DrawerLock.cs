using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawerLock : MonoBehaviour
{
    public string requiredKeyName = "drawerKey"; // Name of the item needed to unlock
    public Text txt;                              // UI prompt
    public InventorySelector selector;
    public HUD hud;
    public AudioSource drawerOpenSound;

    public Transform drawerPart;                  // The drawer piece that slides open
    public Vector3 slideOffset = new Vector3(0.2f, 0, 0); // How far it slides in local space
    public float smooth = 2.0f;
    public float positionTolerance = 0.01f;

    public Collider flashlightCollider;           // Assigned in Inspector
    public Collider knifeCollider;                // Assigned in Inspector

    private bool isPlayerNear = false;
    private bool isUnlocked = false;
    private bool isOpened = false;
    private bool pickupEnabled = false;

    private Vector3 closedPos;
    private Vector3 openPos;

    private Inventory inventory;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();

        if (drawerPart == null)
            drawerPart = transform;

        closedPos = drawerPart.localPosition;
        openPos = closedPos + slideOffset;

        // Ensure item colliders are disabled at start
        if (flashlightCollider != null)
            flashlightCollider.enabled = false;
        if (knifeCollider != null)
            knifeCollider.enabled = false;
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            if (isUnlocked)
            {
                // only call once
                if (!isOpened)
                {
                    OpenDrawer();
                }
            }
            else if (PlayerIsHoldingKey(requiredKeyName))
            {
                isUnlocked = true;
                txt.text = "";
                RemoveHeldKey();
                OpenDrawer();
            }
            else
            {
                txt.text = "wrong key";
            }
        }

        if (isOpened && Vector3.Distance(drawerPart.localPosition, openPos) > positionTolerance)
        {
            drawerPart.localPosition = Vector3.Lerp(drawerPart.localPosition, openPos, Time.deltaTime * smooth);
        }

        if (isOpened && !pickupEnabled && IsFullyOpened)
        {
            EnableItemPickup();
        }
    }


    private void OpenDrawer()
    {
        isOpened = true;
        txt.text = "";
        if (drawerOpenSound != null)
            drawerOpenSound.Play();

        // Enable item pickup after drawer is fully open
        if (flashlightCollider != null)
            flashlightCollider.enabled = true;
        if (knifeCollider != null)
            knifeCollider.enabled = true;
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

    private void RemoveHeldKey()
    {
        int selectedSlot = selector.currentSlot;
        var items = inventory.GetItems();

        if (selectedSlot >= 0 && selectedSlot < items.Count)
        {
            items.RemoveAt(selectedSlot);

            Transform slot = hud.inventoryPanel.GetChild(selectedSlot);
            Image itemImage = slot.Find("border/ItemImage")?.GetComponent<Image>();

            if (itemImage != null)
            {
                itemImage.sprite = null;
                itemImage.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            txt.text = isUnlocked ? "" : "Unlock 'F'";
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

    public bool IsFullyOpened
    {
        get
        {
            return isOpened && Vector3.Distance(drawerPart.localPosition, openPos) <= positionTolerance;
        }
    }

    private void EnableItemPickup()
    {
        pickupEnabled = true;

        if (knifeCollider != null)
            knifeCollider.enabled = true;

        // flashlightCollider stays disabled for now
    }

}
