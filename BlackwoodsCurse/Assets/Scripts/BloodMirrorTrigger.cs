using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BloodMirrorTrigger : MonoBehaviour
{
    public TextMeshProUGUI bloodyMessage; // Assign your TMP text in the inspector
    public AudioSource scareAudio;        // Assign your creepy sound
    private bool triggered = false;

    void Start()
    {
        // Hide the message at start
        if (bloodyMessage != null)
            bloodyMessage.alpha = 0f;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.name);
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;

            if (bloodyMessage != null)
                StartCoroutine(FadeInText(bloodyMessage, 1f)); // fade in over 1 second

            if (scareAudio != null)
                scareAudio.Play();
        }
    }

    private System.Collections.IEnumerator FadeInText(TextMeshProUGUI text, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            text.alpha = Mathf.Lerp(0f, 1f, t / duration);
            yield return null;
        }
        text.alpha = 1f;
    }
}

