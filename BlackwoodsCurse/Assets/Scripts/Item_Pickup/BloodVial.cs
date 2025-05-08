using NavKeypad;
using UnityEngine;

public class BloodVial : MonoBehaviour, IInventoryItem
{
    public string Name { get { return "BloodVial"; } }
    public Sprite _Image = null;
    public Sprite Image { get { return _Image; } }

    private bool isPlayerNear = false;
    private bool hasBeenPickedUp = false;
    private Collider vialCollider;

    void Start()
    {
        vialCollider = GetComponent<Collider>();
        if (vialCollider != null)
            vialCollider.enabled = false;
    }

    private bool CanPickup()
    {
        return SafeDoor.IsSafeFullyOpened && isPlayerNear && !hasBeenPickedUp;
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F pressed near BloodVial");
            if (CanPickup())
            {
                Debug.Log("CanPickup() returned true for BloodVial");
                TryPickup();
            }
            else
            {
                Debug.Log("CanPickup() returned FALSE for BloodVial");
            }
        }
    }

    public void OnPickup()
    {
        Debug.LogWarning("BloodVial OnPickup() was called externally but blocked.");
    }

    private void TryPickup()
    {
        Debug.Log("TryPickup() called");

        if (hasBeenPickedUp || !CanPickup())
        {
            Debug.Log("Blocked: already picked up or not eligible.");
            return;
        }

        hasBeenPickedUp = true;

        Debug.Log("Picked up BloodVial — disabling object.");
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = false;
    }

    public void EnablePickupExternally()
    {
        if (vialCollider != null)
            vialCollider.enabled = true;

        Debug.Log("BloodVial pickup is now enabled (via animation event).");
    }
}
