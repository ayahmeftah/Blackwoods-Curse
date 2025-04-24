using UnityEngine;

public class Flashlight : MonoBehaviour, IInventoryItem
{
    public string Name { get { return "Flashlight"; } }
    public Sprite _Image = null;
    public Sprite Image { get { return _Image; } }

    public DrawerLock drawerLock;
    private bool isPlayerNear = false;
    private bool hasBeenPickedUp = false;
    private Inventory inventory;

    void Start()
    {
        if (drawerLock == null)
            drawerLock = FindObjectOfType<DrawerLock>();

        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F pressed near flashlight");
            TryPickup();
        }
    }

    public void OnPickup()
    {
        Debug.LogWarning("OnPickup called externally — blocked.");
    }

    private void TryPickup()
    {
        if (hasBeenPickedUp || !CanPickup()) return;

        hasBeenPickedUp = true;

        // Add to inventory
        if (inventory != null)
        {
            inventory.AddItem(this);
            Debug.Log("Flashlight added to inventory.");
        }
        else
        {
            Debug.LogWarning("Inventory not found!");
        }

        gameObject.SetActive(false);
        Debug.Log("Flashlight picked up.");
    }

    private bool CanPickup()
    {
        bool valid = drawerLock != null && drawerLock.IsFullyOpened && isPlayerNear;
        Debug.Log($"Flashlight CanPickup: {valid}");
        return valid;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player is near Flashlight");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player left Flashlight trigger");
        }
    }
}
