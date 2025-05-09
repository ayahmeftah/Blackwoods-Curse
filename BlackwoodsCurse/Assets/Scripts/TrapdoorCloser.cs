using UnityEngine;
using System.Collections;

public class TrapdoorCloser : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The script that currently opens the door")]
    public Trapdoor trapdoorScript;

    [Tooltip("The hinge object that we animate")]
    public Transform trapdoorHinge;

    [Tooltip("HUD reference for showing messages")]
    public HUD hud;

    [Header("Settings")]
    [Tooltip("How fast it slams shut")]
    public float closeSpeed = 3f;

    [Tooltip("Optional: a heavy slam sound")]
    public AudioSource slamSound;

    [Tooltip("How long to show the message")]
    public float messageDuration = 2f;

    private bool hasClosed = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasClosed && other.CompareTag("Player"))
        {
            hasClosed = true;

            //Play the sound at the very beginning
            if (slamSound != null) 
            {
                slamSound.Play();
            }

            //Show the message "Door is Locked"
            hud.txt.text = "The door is locked.";
            StartCoroutine(HideMessageAfterDelay());

            StartCoroutine(CloseAnimation());
        }
    }

    IEnumerator CloseAnimation()
    {
        // animate from open (90°) back down to 0°
        Quaternion start = trapdoorHinge.localRotation;
        Quaternion end   = Quaternion.Euler(0, 0, 0);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * closeSpeed;
            trapdoorHinge.transform.localRotation = Quaternion.Slerp(start, end, t);
            yield return null;
        }
        trapdoorHinge.transform.localRotation = end;

        // now disable the open/close script so it cannot be used again
        if (trapdoorScript != null)
            trapdoorScript.enabled = false;
    }

    IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration);
        hud.HideMessage();
    }
}
