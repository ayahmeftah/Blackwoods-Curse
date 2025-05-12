using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyboardPianoInput : MonoBehaviour
{
    public ZoneSwitcher zoneSwitcher;
    public PianoInteractionTrigger pianoTrigger;

    [System.Serializable]
    public class PianoKey
    {
        public KeyCode letterKey; // e.g., KeyCode.C
        public string noteName;   // "C"

        public AudioClip noteSoundZone1;
        public AudioClip noteSoundZone2;
        public AudioClip noteSoundZone3;

        public TextMeshProUGUI labelZone1;
        public TextMeshProUGUI labelZone2;
        public TextMeshProUGUI labelZone3;
    }

    public List<PianoKey> pianoKeys = new List<PianoKey>();
    public AudioSource audioSource;

    void Update()
    {
        if (!pianoTrigger.InPianoMode) return;

        foreach (PianoKey key in pianoKeys)
        {
            if (Input.GetKeyDown(key.letterKey))
            {
                PlayNoteSound(key);

                TextMeshProUGUI labelToFlash = null;
                switch (zoneSwitcher.activeZone)
                {
                    case 1: labelToFlash = key.labelZone1; break;
                    case 2: labelToFlash = key.labelZone2; break;
                    case 3: labelToFlash = key.labelZone3; break;
                }

                if (labelToFlash != null)
                    StartCoroutine(FlashKeyText(labelToFlash));

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

    IEnumerator FlashKeyText(TextMeshProUGUI text)
    {
        Color originalColor = text.color;
        text.color = Color.yellow;
        yield return new WaitForSeconds(0.3f);
        text.color = originalColor;
    }
}
