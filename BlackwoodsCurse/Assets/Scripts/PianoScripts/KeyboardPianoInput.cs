using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // If you're using legacy Text
// using TMPro; // Uncomment if using TextMeshProUGUI

public class KeyboardPianoInput : MonoBehaviour
{
    public ZoneSwitcher zoneSwitcher;

    [System.Serializable]
    public class PianoKey
    {
        public KeyCode letterKey; // KeyCode.C, KeyCode.D, etc.
        public string noteName;   // "C", "D", etc.
        public AudioClip noteSoundZone1;
        public AudioClip noteSoundZone2;
        public AudioClip noteSoundZone3;
        public Text labelText; // ← For UnityEngine.UI.Text
        // public TextMeshProUGUI labelText; // ← If using TMPro
    }

    public List<PianoKey> pianoKeys = new List<PianoKey>();
    public AudioSource audioSource;

    void Update()
    {
        foreach (PianoKey key in pianoKeys)
        {
            if (Input.GetKeyDown(key.letterKey))
            {
                PlayNoteSound(key);
                StartCoroutine(FlashKeyText(key.labelText));
                break;
            }
        }
    }

    void PlayNoteSound(PianoKey key)
    {
        AudioClip clip = null;
        switch (zoneSwitcher.activeZone)
        {
            case 1: clip = key.noteSoundZone1; break;
            case 2: clip = key.noteSoundZone2; break;
            case 3: clip = key.noteSoundZone3; break;
        }

        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    IEnumerator FlashKeyText(Text text)
    {
        Color originalColor = text.color;
        text.color = Color.yellow;
        yield return new WaitForSeconds(0.3f);
        text.color = originalColor;
    }
}
