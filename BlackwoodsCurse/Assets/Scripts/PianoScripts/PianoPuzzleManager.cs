using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// ...



public class PianoPuzzleManager : MonoBehaviour
{
    public List<string> correctSequence = new List<string> {
        "E1", "G1", "C1", "A1", "B1", "F1", "G2", "C2"
    };

    public List<Image> starImages; // Assign 8 star UI images in order
    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;
    public Color neutralColor = Color.gray;

    

    public TextMeshProUGUI resetMessage;

    private List<string> currentInput = new List<string>();
    private bool puzzleFailed = false;

    public List<MeshRenderer> pictureRenderers;       // your 4 picture objects
public List<Material> solvedMaterials;            // your 4 custom solved materials
public List<GameObject> glowObjects;              // optional glowing borders

public List<GameObject> solvedPictureObjects; // the new solved image objects


    public void NotePlayed(string note)
    {
        if (puzzleFailed || currentInput.Count >= correctSequence.Count)
            return;

        currentInput.Add(note);

        int index = currentInput.Count - 1;
        if (note == correctSequence[index])
        {
            starImages[index].color = correctColor;

            if (currentInput.Count == correctSequence.Count)
            {
                Debug.Log("Puzzle complete!");
                // TODO: Trigger puzzle completion event
            }
        }
        else
        {
            puzzleFailed = true;
            starImages[index].color = wrongColor;
            ShowResetMessage();
        }

        if (currentInput.Count == correctSequence.Count)
{
    Debug.Log("Puzzle complete!");
    UpdateWallPictures(); // 👈 this will be called
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
}




}

