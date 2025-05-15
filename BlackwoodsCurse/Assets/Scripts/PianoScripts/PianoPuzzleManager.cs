using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PianoPuzzleManager : MonoBehaviour
{
    public HUD hud;

    // Singleton instance
    public static PianoPuzzleManager Instance { get; private set; }

    // Puzzle sequence
    public List<string> correctSequence = new List<string> {
        "E1", "G1", "C1", "A1", "B1", "F1", "G2", "C2"
    };

    // UI and visual feedback
    public List<Image> starImages; // Assign 8 star UI images in order
    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;
    public Color neutralColor = Color.gray;
    public TextMeshProUGUI resetMessage;

    // Puzzle tracking
    private List<string> currentInput = new List<string>();
    private bool puzzleFailed = false;
    private bool puzzleSolved = false;
    public bool IsPuzzleSolved => puzzleSolved;

    // Environment changes
    public List<MeshRenderer> pictureRenderers;
    public List<GameObject> glowObjects;
    public List<GameObject> solvedPictureObjects;
    public GameObject peekBook;
    public ParticleSystem sparkleEffect;

    public PianoInteractionTrigger pianoTrigger; // Assign this too in Inspector

    void Awake()
    {
        Instance = this;
    }

    public void NotePlayed(string note)
    {
        if (puzzleFailed || puzzleSolved || currentInput.Count >= correctSequence.Count)
            return;

        currentInput.Add(note);

        int index = currentInput.Count - 1;
        if (note == correctSequence[index])
        {
            starImages[index].color = correctColor;

            if (currentInput.Count == correctSequence.Count)
            {
                puzzleSolved = true;
                Debug.Log("Puzzle complete!");
                UpdateWallPictures();
            }
        }
        else
        {
            puzzleFailed = true;
            starImages[index].color = wrongColor;
            ShowResetMessage();
        }
    }

    void ShowResetMessage()
    {
        if (resetMessage != null)
            resetMessage.gameObject.SetActive(true);
    }

    void HideResetMessage()
    {
        if (resetMessage != null)
            resetMessage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (puzzleFailed && Input.GetKeyDown(KeyCode.R))
        {
            ResetPuzzle();
        }
    }

    public void ResetPuzzle()
    {
        currentInput.Clear();
        puzzleFailed = false;

        for (int i = 0; i < starImages.Count; i++)
        {
            starImages[i].color = neutralColor;
        }

        HideResetMessage();
    }

    public int CurrentZone => currentInput.Count > 0 ? GetZoneFromNote(currentInput[^1]) : -1;

    private int GetZoneFromNote(string note)
    {
        if (note.Length < 2) return -1;
        int zone;
        if (int.TryParse(note[^1].ToString(), out zone))
            return zone;
        return -1;
    }

    void UpdateWallPictures()
    {
        for (int i = 0; i < solvedPictureObjects.Count; i++)
        {
            if (solvedPictureObjects[i] != null)
                solvedPictureObjects[i].SetActive(true);

            if (i < glowObjects.Count && glowObjects[i] != null)
                glowObjects[i].SetActive(true);
        }

        if (peekBook != null && !peekBook.activeSelf)
        {
            peekBook.SetActive(true);
            peekBook.transform.position += new Vector3(0.05f, 0f, 0f);
        }

        if (sparkleEffect != null)
            sparkleEffect.Play();

        if (hud != null && hud.txt != null)
        {
            StartCoroutine(ShowSuccessMessage());
        }

        if (pianoTrigger != null)
        {
            Invoke(nameof(ExitPianoModeAfterSuccess), 1.5f);
            LevelManager.Instance.AdvanceLevel();
        }
    }

    void ExitPianoModeAfterSuccess()
    {
        if (pianoTrigger != null)
            pianoTrigger.ExitPianoMode();

        if (hud != null)
            hud.HideMessage();
    }

    IEnumerator ShowSuccessMessage()
    {
        hud.txt.color = Color.green;
        yield return new WaitForSeconds(2f);
        hud.txt.color = Color.white;
        hud.HideMessage();
    }
}
