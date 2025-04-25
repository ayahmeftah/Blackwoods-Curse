using UnityEngine;
using UnityEngine.UI;

public class StudyBookcaseMove : MonoBehaviour
{
    public string bookObjectName = "book";
    public string bookcaseObjectName = "Wardrobe_01 (1)";
    public float delayBetween = 1f;
    public Text txt;

    private Animator bookAnimator;
    private Animator bookcaseAnimator;

    private bool isPlayerNear = false;
    private bool hasActivated = false;

    void Start()
    {
        // Automatically find the animators by GameObject name

        // Reset transform in case animation pushed it early
        GameObject book = GameObject.Find(bookObjectName);
        if (book != null)
        {
            book.transform.localRotation = Quaternion.identity;
            book.transform.localPosition = Vector3.zero; // if needed
        }

        GameObject bookcase = GameObject.Find(bookcaseObjectName);

        if (book != null)
            bookAnimator = book.GetComponent<Animator>();
        else
            Debug.LogWarning("Book object not found!");

        if (bookcase != null)
            bookcaseAnimator = bookcase.GetComponent<Animator>();
        else
            Debug.LogWarning("Bookcase object not found!");

        if (txt != null)
            txt.text = "";
    }

    void Update()
    {
        if (isPlayerNear && !hasActivated && Input.GetKeyDown(KeyCode.F))
        {
            hasActivated = true;

            if (txt != null)
                txt.text = "";

            if (bookAnimator != null)
                bookAnimator.SetTrigger("Pull");

            Invoke(nameof(TriggerBookcaseOpen), delayBetween);
        }
    }

    void TriggerBookcaseOpen()
    {
        bookcaseAnimator?.SetTrigger("Open");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasActivated)
        {
            isPlayerNear = true;
            if (txt != null)
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
}
