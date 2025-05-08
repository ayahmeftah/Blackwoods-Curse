using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NavKeypad
{
    public class Keypad : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private UnityEvent onAccessGranted;
        [SerializeField] private UnityEvent onAccessDenied;
        [Header("Combination Code (9 Numbers Max)")]
        [SerializeField] private int keypadCombo = 12345;

        public UnityEvent OnAccessGranted => onAccessGranted;
        public UnityEvent OnAccessDenied => onAccessDenied;

        [Header("Settings")]
        [SerializeField] private string accessGrantedText = "Safe Unlocked";
        [SerializeField] private string accessDeniedText = "Wrong Password";

        [Header("Visuals")]
        [SerializeField] private float displayResultTime = 1f;
        [Range(0, 5)]
        [SerializeField] private float screenIntensity = 2.5f;

        [Header("Colors")]
        [SerializeField] private Color screenNormalColor = new Color(0.98f, 0.50f, 0.032f, 1f); // orangy
        [SerializeField] private Color screenDeniedColor = new Color(1f, 0f, 0f, 1f); // red
        [SerializeField] private Color screenGrantedColor = new Color(0f, 0.62f, 0.07f); // greenish

        [Header("SoundFx")]
        [SerializeField] private AudioClip buttonClickedSfx;
        [SerializeField] private AudioClip accessDeniedSfx;
        [SerializeField] private AudioClip accessGrantedSfx;

        [Header("Component References")]
        [SerializeField] private Renderer panelMesh;
        [SerializeField] private TMP_Text keypadDisplayText;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Animator safeAnimator;
        private bool animatorWasEnabled = false;


        private string currentInput;
        private bool displayingResult = false;
        private bool accessWasGranted = false;
        private bool isPlayerNear = false;
        private bool allowKeypadInput = false;

        public Text txt;

        private void Awake()
        {
            ClearInput();
            panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity);

            if (safeAnimator != null)
                safeAnimator.enabled = false; // <- disable animator at start
        }

        public void AddInput(string input)
        {
            audioSource.PlayOneShot(buttonClickedSfx);
            if (!allowKeypadInput || displayingResult || accessWasGranted) return;

            switch (input)
            {
                case "enter":
                    CheckCombo();
                    break;
                default:
                    if (currentInput != null && currentInput.Length == 9) return;
                    currentInput += input;
                    keypadDisplayText.text = currentInput;
                    break;
            }
        }

        public void CheckCombo()
        {
            Debug.Log("Checking combo: " + currentInput + " vs " + keypadCombo);

            if (int.TryParse(currentInput, out var currentKombo))
            {
                Debug.Log("Parsed input as: " + currentKombo);
                bool granted = currentKombo == keypadCombo;
                Debug.Log("Access granted? " + granted);

                if (!displayingResult)
                {
                    StartCoroutine(DisplayResultRoutine(granted));
                }
            }
            else
            {
                Debug.LogWarning("Couldn't process input string: " + currentInput);
            }
        }

        private IEnumerator DisplayResultRoutine(bool granted)
        {
            displayingResult = true;

            if (granted)
            {
                accessWasGranted = true;
                keypadDisplayText.text = accessGrantedText;
                panelMesh.material.SetVector("_EmissionColor", screenGrantedColor * screenIntensity);
                audioSource.PlayOneShot(accessGrantedSfx);

                OnAccessGranted?.Invoke(); // <- This triggers BloodVial pickup after 1 sec

                yield return new WaitForSeconds(displayResultTime);

                if (safeAnimator != null && !animatorWasEnabled)
                {
                    safeAnimator.enabled = true;
                    animatorWasEnabled = true;
                    Invoke(nameof(TriggerSafeOpen), 0.01f);
                }
            }
            else
            {
                AccessDenied();
                yield return new WaitForSeconds(displayResultTime);
                ClearInput();
                panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity);
            }

            displayingResult = false;
        }

        private void AccessDenied()
        {
            keypadDisplayText.text = accessDeniedText;
            onAccessDenied?.Invoke();
            panelMesh.material.SetVector("_EmissionColor", screenDeniedColor * screenIntensity);
            audioSource.PlayOneShot(accessDeniedSfx);
        }

        private void ClearInput()
        {
            currentInput = "";
            keypadDisplayText.text = currentInput;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerNear = true;
                if (allowKeypadInput)
                    txt.text = "Enter Code";
                else
                    txt.text = "";
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerNear = false;
                if (txt != null)
                    txt.text = ""; // Clear message
            }
        }

        private void Update()
        {
            if (!isPlayerNear || displayingResult || accessWasGranted || !allowKeypadInput) return;

            foreach (var key in "0123456789")
            {
                if (Input.GetKeyDown(key.ToString()))
                {
                    AddInput(key.ToString());
                }
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if (currentInput.Length > 0)
                {
                    currentInput = currentInput.Substring(0, currentInput.Length - 1);
                    keypadDisplayText.text = currentInput;
                }
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                AddInput("enter");
            }
        }

        private void TriggerSafeOpen()
        {
            if (safeAnimator != null)
                safeAnimator.SetTrigger("OpenSafe");
        }

        public void EnableInput()
        {
            allowKeypadInput = true;
            Debug.Log("Keypad input now enabled.");
        }

    }
}
