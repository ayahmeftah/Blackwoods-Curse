using NavKeypad;
using UnityEngine;
using UnityEngine.UI;

public class StudyBookcaseMove : MonoBehaviour
{
    public string bookcaseObjectName = "Wardrobe_01 (1)";
    public float delayBetween = 1f;
    public Text txt;

    private Animator bookcaseAnimator;

    private bool isPlayerNear = false;
    private bool hasActivated = false;

    public Keypad keypadScript;

    void Start()
    {
        GameObject bookcase = GameObject.Find(bookcaseObjectName);

        if (bookcase != null)
        {
            bookcaseAnimator = bookcase.GetComponent<Animator>();

            // Disable Animator completely at start
            bookcaseAnimator.enabled = false;
        }
        else
        {
            Debug.LogWarning("Bookcase object not found!");
        }

        if (txt != null)
            txt.text = "";
    }

    void Update()
    {
        if (isPlayerNear && !hasActivated && Input.GetKeyDown(KeyCode.F))
        {
            if (DrawerLock.isDrawerUnlocked) // Only allow if drawer is unlocked
            {
                hasActivated = true;

                if (txt != null)
                    txt.text = "";

                if (bookcaseAnimator != null)
                {
                    // Enable animator now that we're ready
                    bookcaseAnimator.enabled = true;

                    // Give it a frame to initialize
                    Invoke(nameof(TriggerBookcaseRotate), 0.01f);
                }
            }
            else
            {
                Debug.Log("Drawer is still locked! Cannot pull the book yet.");
                if (txt != null)
                    txt.text = "The drawer is still locked...";
            }
        }
    }

    void TriggerBookcaseRotate()
    {
        Debug.Log("Triggering bookcase rotation...");
        if (bookcaseAnimator != null)
        {
            bookcaseAnimator.SetBool("AllowOpen", true);
            bookcaseAnimator.SetTrigger("Pull");
            Invoke(nameof(AllowKeypadInput), 1.0f); // wait 1 second after animation starts
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasActivated)
        {
            isPlayerNear = true;
            if (txt != null && DrawerLock.isDrawerUnlocked)
                txt.text = "Press F to pull";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (txt != null)
                txt.text = "";
        }
    }

    private void AllowKeypadInput()
    {
        if (keypadScript != null)
            keypadScript.EnableInput();
    }

}