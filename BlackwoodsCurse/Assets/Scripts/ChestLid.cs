using System.Collections;
using UnityEngine;

public class ChestLid : MonoBehaviour
{
    public HUD hud;
    public GameObject hammer;     // hammer inside the chest
    public float openSpeed = 60f; // speed of lid opening

    private bool playerNear = false;
    private bool canOpen = false;
    private bool isOpened = false;
    private Quaternion targetRotation;

    void Start()
    {
        hammer.SetActive(false); // hidden until chest is open
        targetRotation = Quaternion.Euler(-90f, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    public void UnlockChest()
    {
        canOpen = true;
    }

    void Update()
    {
        if (!canOpen || isOpened || !playerNear) return;

        hud.txt.text = "Press F to open chest";

        if (Input.GetKeyDown(KeyCode.F))
        {
            isOpened = true;
            StartCoroutine(OpenLid());
        }
    }

    IEnumerator OpenLid()
    {
        // Smoothly rotate the lid open
        while (Quaternion.Angle(transform.localRotation, targetRotation) > 1f)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, openSpeed * Time.deltaTime);
            yield return null;
        }

        transform.localRotation = targetRotation;

        // ✅ Show hammer after lid is fully open
        hammer.SetActive(true);

        // ✅ Show success message briefly then hide
        hud.txt.text = "Chest opened!";
        yield return new WaitForSeconds(2f);
        hud.HideMessage();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNear = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            hud.HideMessage();
        }
    }
}
