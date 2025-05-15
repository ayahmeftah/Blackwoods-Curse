using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StoreroomKeypad : MonoBehaviour
{
    public StoreroomDoorAyah storeroomDoor;

    [Header("Code Settings")]
    public string correctCode = "4212";

    [Header("UI")]
    public TMP_Text displayText;
    public HUD hud;

    [Header("Messages")]
    public string accessGrantedText = "Door Opened";
    public string accessDeniedText = "Wrong Password";
    public string restrictedText = "Restricted: Complete Level 4 First!";

    [Header("Colors")]
    public Color normalColor = Color.cyan;
    public Color grantedColor = Color.green;
    public Color deniedColor = Color.red;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip deniedSound;
    public AudioClip grantedSound;

    [Header("Visual Settings")]
    public Renderer panelMesh;
    public float screenIntensity = 2.5f;
    public float displayResultTime = 1.5f;

    [Header("Events")]
    public UnityEvent OnAccessGranted;

    private string currentInput = "";
    private bool isPlayerNear = false;
    private bool isUnlocked = false;
    private bool displayingResult = false;
    private bool hasShownRestrictedMessage = false;

    private bool puzzleSolved => PianoPuzzleManager.Instance != null && PianoPuzzleManager.Instance.IsPuzzleSolved;

    private void Start()
    {
        UpdateScreenColor(normalColor);
        if (displayText != null) displayText.text = "";
    }

    private void Update()
    {
        if (!isPlayerNear || displayingResult || isUnlocked || !puzzleSolved)
            return;

        foreach (var key in "0123456789")
        {
            if (Input.GetKeyDown(key.ToString()))
                AddInput(key.ToString());
        }

        if (Input.GetKeyDown(KeyCode.Backspace) && currentInput.Length > 0)
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
            displayText.text = currentInput;
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CheckCode();
        }
    }

    private void AddInput(string digit)
    {
        if (!puzzleSolved) return;

        if (audioSource && clickSound) audioSource.PlayOneShot(clickSound);
        if (currentInput.Length < 9)
        {
            currentInput += digit;
            displayText.text = currentInput;
        }
    }

    private void CheckCode()
    {
        if (!puzzleSolved) return;

        if (currentInput == correctCode)
        {
            StartCoroutine(ShowResult(true));
        }
        else
        {
            StartCoroutine(ShowResult(false));
        }
    }

    private IEnumerator ShowResult(bool success)
    {
        displayingResult = true;

        //if (success)
        //{
        //    isUnlocked = true;
        //    displayText.text = accessGrantedText;
        //    UpdateScreenColor(grantedColor);
        //    if (audioSource && grantedSound) audioSource.PlayOneShot(grantedSound);

        //    OnAccessGranted?.Invoke();

        //    yield return new WaitForSeconds(displayResultTime);

        //    if (hud != null) hud.HideMessage();

        //    // ✅ Show Open F only if player is near the keypad and door
        //    if (isPlayerNear && storeroomDoor != null)
        //        storeroomDoor.ShowOpenMessage();
        //}
        if (success)
        {
            isUnlocked = true;
            displayText.text = accessGrantedText;
            UpdateScreenColor(grantedColor);
            if (audioSource && grantedSound) audioSource.PlayOneShot(grantedSound);

            OnAccessGranted?.Invoke();

            yield return new WaitForSeconds(displayResultTime);

            if (hud != null) hud.HideMessage();

            if (isPlayerNear && storeroomDoor != null)
            {
                storeroomDoor.OpenAndLockDoor(); // Door opens automatically and locks
            }
        }
        else
        {
            displayText.text = accessDeniedText;
            UpdateScreenColor(deniedColor);
            if (audioSource && deniedSound) audioSource.PlayOneShot(deniedSound);

            yield return new WaitForSeconds(displayResultTime);
            displayText.text = "";
            currentInput = "";
            UpdateScreenColor(normalColor);
        }

        displayingResult = false;
    }

    private void UpdateScreenColor(Color color)
    {
        if (panelMesh)
            panelMesh.material.SetVector("_EmissionColor", color * screenIntensity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerNear = true;

        if (isUnlocked)
        {
            if (storeroomDoor != null)
                storeroomDoor.ShowOpenMessage();
        }
        else if (!puzzleSolved && !hasShownRestrictedMessage)
        {
            hasShownRestrictedMessage = true;
            if (hud != null && hud.txt != null)
            {
                hud.txt.color = Color.white;
                hud.txt.text = restrictedText;
                StartCoroutine(ClearHUDMessage(2f));
            }
        }
        else if (puzzleSolved)
        {
            if (hud != null && hud.txt != null)
            {
                hud.txt.color = Color.white;
                hud.txt.text = "Enter Code";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            hasShownRestrictedMessage = false;
            if (hud != null)
                hud.HideMessage();
        }
    }

    private IEnumerator ClearHUDMessage(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (hud != null && isPlayerNear)
        {
            hud.txt.color = Color.white;
            hud.HideMessage();
        }
    }
}
