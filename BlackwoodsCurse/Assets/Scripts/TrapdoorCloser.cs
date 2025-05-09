using UnityEngine;
using System.Collections;

public class TrapdoorCloser : MonoBehaviour
{
    [Tooltip("The script that currently opens the door")]
    public Trapdoor trapdoorScript;

    [Tooltip("The hinge object that we animate")]
    public Transform trapdoorHinge;

    [Tooltip("How fast it slams shut")]
    public float closeSpeed = 3f;

    [Tooltip("Optional: a heavy slam sound")]
    public AudioSource slamSound;

    bool hasClosed = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasClosed && other.CompareTag("Player"))
        {
            hasClosed = true;

            // 🔊 Play the sound at the very beginning
            if (slamSound != null) 
            {
                slamSound.Play();
            }

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
            trapdoorHinge.localRotation = Quaternion.Slerp(start, end, t);
            yield return null;
        }
        trapdoorHinge.localRotation = end;

        // now disable the open/close script so it cannot be used again
        if (trapdoorScript != null)
            trapdoorScript.enabled = false;
    }
}
