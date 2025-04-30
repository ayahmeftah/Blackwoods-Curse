using UnityEngine;

public class Knife : MonoBehaviour, IInventoryItem
{
    public string Name { get { return "Knife"; } }
    public Sprite _Image = null;
    public Sprite Image { get { return _Image; } }

    public DrawerLock drawerLock;
    private bool isPlayerNear = false;
    private bool hasBeenPickedUp = false;

    void Start()
    {
        if (drawerLock == null)
            drawerLock = FindObjectOfType<DrawerLock>();
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            TryPickup();
        }
    }

    public void OnPickup()
    {
        // Don't allow external systems to trigger pickup directly
        Debug.LogWarning("Knife OnPickup() was called externally but blocked to prevent double pickup.");
    }

    private void TryPickup()
    {
        if (hasBeenPickedUp || !CanPickup()) return;

        hasBeenPickedUp = true;
        gameObject.SetActive(false);
        Debug.Log("Picked up Knife.");

        if (drawerLock != null && drawerLock.flashlightCollider != null)
        {
            drawerLock.flashlightCollider.enabled = true;
            Debug.Log("Flashlight collider enabled");
        }
    }

    private bool CanPickup()
    {
        return drawerLock != null && drawerLock.IsFullyOpened && drawerLock.PickupEnabled && isPlayerNear;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerNear = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerNear = false;
    }
}