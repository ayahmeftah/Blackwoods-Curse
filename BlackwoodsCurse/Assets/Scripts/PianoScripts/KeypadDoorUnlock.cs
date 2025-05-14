using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KeypadDoorUnlock : MonoBehaviour
{
    public float openAngle = 90f;
    public float smooth = 2f;
    public HUD hud; // ✅ Added HUD to show message

    private Quaternion defaultRot;
    private Quaternion openRot;
    private bool shouldOpen = false;
    private bool isOpen = false;

    public AudioSource doorSound;

    void Start()
    {
        defaultRot = transform.rotation;
        openRot = Quaternion.Euler(defaultRot.eulerAngles + Vector3.up * openAngle);
    }

    void Update()
    {
        if (shouldOpen && !isOpen && Quaternion.Angle(transform.rotation, openRot) > 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRot, Time.deltaTime * smooth);
        }
    }

    public void UnlockDoor()
    {
        shouldOpen = true;
        isOpen = true;

        if (hud != null && hud.txt != null)
        {
            hud.txt.color = Color.white;
            hud.txt.text = "Open F";
            StartCoroutine(HideAfterDelay());
        }

        if (doorSound != null)
            doorSound.Play();
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        if (hud != null)
            hud.HideMessage();
    }
}
