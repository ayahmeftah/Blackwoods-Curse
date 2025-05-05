using System.Collections;
using UnityEngine;

public class KeyVaseInteraction : MonoBehaviour
{
    public GameObject keyObject;                // Drag the BedroomKey here
    public Transform floatTarget;               // Empty GameObject in front of vase
    public Inventory inventory;                 // Reference to Inventory script
    public InventorySelector selector;          // Reference to inventory selector
    public HUD hud;                             // Reference to HUD

    private bool isPlayerNear = false;
    private bool hasFloated = false;

    void Update()
    {
        if (isPlayerNear && !hasFloated)
        {
            var items = inventory.GetItems();
            int index = selector.currentSlot;

            if (index < items.Count && items[index].Name == "RopeMagnet")
            {
                hud.txt.text = "Pull Key P";

                if (Input.GetKeyDown(KeyCode.P))
                {
                    StartCoroutine(FloatKey());
                    hasFloated = true;
                }
            }
        }
    }

    IEnumerator FloatKey()
{
    // Disable visuals first to hide snap
    keyObject.SetActive(true);
    Renderer rend = keyObject.GetComponent<Renderer>();
    if (rend != null) rend.enabled = false;

    hud.HideMessage();

    // Start from inside vase
    Vector3 start = keyObject.transform.position;
    Vector3 end = floatTarget.position;

    float duration = 1.5f;
    float elapsed = 0f;

    // Wait one frame to prevent visual pop
    yield return null;

    // Now smoothly move the key
    while (elapsed < duration)
    {
        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / duration);
        keyObject.transform.position = Vector3.Lerp(start, end, t);
        yield return null;
    }

    // Lock final position and re-show the key
keyObject.transform.position = end;
if (rend != null) rend.enabled = true;

// 🧩 NEW FIXES
keyObject.transform.SetParent(null); // In case it's parented
var rb = keyObject.GetComponent<Rigidbody>();
if (rb != null)
{
    rb.velocity = Vector3.zero;
    rb.isKinematic = true;
}

// Optional float effect
keyObject.GetComponent<KeyFloatEffect>()?.SetStartNow();

// Show pickup prompt
hud.txt.text = "Pickup F";

// 🧹 Remove RopeMagnet from inventory — it was used
var items = inventory.GetItems();
int selected = selector.currentSlot;
if (selected < items.Count && items[selected].Name == "RopeMagnet")
{
    inventory.RemoveItemAtSlot(selected);
}


}



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            hud.HideMessage();
        }
    }
}
