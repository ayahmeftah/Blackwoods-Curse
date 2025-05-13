using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StoreroomDoor : MonoBehaviour
{
    public float openAngle = 90f;
    public float smooth = 2f;
    public HUD hud; // To show "Open F"

    private Quaternion defaultRot;
    private Quaternion openRot;
    private bool playerNear = false;
    private bool doorUnlocked = false;
    private bool doorOpen = false;

    public AudioSource doorSound;
    public Text txt; // Optional if you still use Text directly

    void Start()
    {
        defaultRot = transform.rotation;
        openRot = Quaternion.Euler(defaultRot.eulerAngles + Vector3.up * openAngle);
        if (hud != null)
            hud.HideMessage();
    }

    void Update()
    {
        if (playerNear && doorUnlocked && Input.GetKeyDown(KeyCode.F))
        {
            doorOpen = true;
            if (hud != null)
                hud.HideMessage();

            if (doorSound != null)
                doorSound.Play();
        }

        if (doorOpen && Quaternion.Angle(transform.rotation, openRot) > 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRot, Time.deltaTime * smooth);
        }
    }

    public void UnlockDoor()
    {
        doorUnlocked = true;
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

    public void ShowOpenMessage()
{
    if (playerNear && hud != null)
    {
        hud.txt.color = Color.white;
        hud.txt.text = "Open F";
    }
}

}
