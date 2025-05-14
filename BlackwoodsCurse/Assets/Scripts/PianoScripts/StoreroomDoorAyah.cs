using UnityEngine;
using UnityEngine.UI;

public class StoreroomDoorAyah : MonoBehaviour
{
    public float openAngle = 90f;
    public float smooth = 2f;
    public HUD hud;
    public AudioSource doorSound;
    public Text txt;

    private Quaternion defaultRot;
    private Quaternion openRot;
    private bool playerNear = false;
    private bool doorUnlocked = false;
    private bool doorOpen = false;
    private bool autoClosing = false;

    void Start()
    {
        defaultRot = transform.rotation;
        openRot = Quaternion.Euler(defaultRot.eulerAngles + Vector3.up * openAngle);
        if (hud != null)
            hud.HideMessage();
    }

    void Update()
    {
        // Manual open
        if (playerNear && doorUnlocked && Input.GetKeyDown(KeyCode.F))
        {
            doorOpen = true;
            if (hud != null) hud.HideMessage();
            if (doorSound != null) doorSound.Play();
        }

        // Smooth opening
        if (doorOpen && Quaternion.Angle(transform.rotation, openRot) > 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRot, Time.deltaTime * smooth);
        }

        // Smooth closing
        if (autoClosing && Quaternion.Angle(transform.rotation, defaultRot) > 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, defaultRot, Time.deltaTime * smooth);
        }
        else if (autoClosing && Quaternion.Angle(transform.rotation, defaultRot) <= 1f)
        {
            autoClosing = false;
        }
    }

    public void OpenAndLockDoor()
    {
        doorOpen = true;
        doorUnlocked = false;
        if (doorSound != null) doorSound.Play();
    }

    public void LockManually()
    {
        doorUnlocked = false;
    }

    public void AutoCloseAndLock()
    {
        autoClosing = true;
        doorOpen = false;
        doorUnlocked = false;
        if (doorSound != null) doorSound.Play();
    }

    public void UnlockDoor()
    {
        doorUnlocked = true;
    }

    public void ShowOpenMessage()
    {
        if (playerNear && hud != null)
        {
            hud.txt.color = Color.white;
            hud.txt.text = "Open F";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;

            if (doorUnlocked && hud != null)
            {
                hud.txt.color = Color.white;
                hud.txt.text = "Open F";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            if (hud != null)
                hud.HideMessage();
        }
    }
}
