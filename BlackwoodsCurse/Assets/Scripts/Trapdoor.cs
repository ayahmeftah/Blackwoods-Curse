using UnityEngine;
using System.Collections;

public class Trapdoor : MonoBehaviour
{
    public HUD hud;
    public Inventory inventory;
    public InventorySelector selector;
    public GameObject trapdoorHinge;
    public float openSpeed = 2f;
    public AudioSource creakSound;
    public string requiredItemName = "Crowbar";

    bool isPlayerNear, isOpened, showingTempMessage, isBoxMoved;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isBoxMoved && !isOpened)
        {
            isPlayerNear = true;
            UpdateMessage();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            hud.HideMessage();
        }
    }

    void Update()
    {
        if (isPlayerNear && !isOpened && Input.GetKeyDown(KeyCode.F))
        {
            if (InventoryHasCrowbar())
                OpenTrapdoor();
            else if (!showingTempMessage)
                StartCoroutine(TempMsg("That is not strong enough...", 1.5f));
        }
    }

    void UpdateMessage()
    {
        if (showingTempMessage || isOpened) return;
        hud.txt.text = InventoryHasCrowbar()
            ? "Press F to Pry Open the Trapdoor"
            : "You need something strong to pry it open.";
    }

    bool InventoryHasCrowbar()
    {
        int s = selector.currentSlot;
        var items = inventory.GetItems();
        return s >= 0 && s < items.Count && items[s].Name == requiredItemName;
    }

    void OpenTrapdoor()
    {
        isOpened = true;
        hud.HideMessage();
        StartCoroutine(OpenAnim());
        creakSound?.Play();
    }

    IEnumerator OpenAnim()
    {
        Quaternion start = trapdoorHinge.transform.localRotation;
        Quaternion end = Quaternion.Euler(90, 0, 0);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * openSpeed;
            trapdoorHinge.transform.localRotation = Quaternion.Slerp(start, end, t);
            yield return null;
        }
    }

    IEnumerator TempMsg(string m, float d)
    {
        showingTempMessage = true;
        hud.txt.text = m;
        yield return new WaitForSeconds(d);
        showingTempMessage = false;
        UpdateMessage();
    }

    // ← Call this from BoxMover when the box is done moving
    public void BoxHasMoved()
    {
        isBoxMoved = true;
    }
}
