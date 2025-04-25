using UnityEngine;
using UnityEngine.UI;

public class SecretMechanismController : MonoBehaviour
{
    public string bookObjectName = "Book";
    public string bookcaseObjectName = "StudyBookcase";
    public float delayBetween = 1f;
    public Text txt;

    private Animator bookAnimator;
    private Animator bookcaseAnimator;

    private bool isPlayerNear = false;
    private bool hasActivated = false;

    void Start()
    {
        // Automatically find the animators by GameObject name
        GameObject book = GameObject.Find(bookObjectName);
        GameObject bookcase = GameObject.Find(bookcaseObjectName);

        if (book != null)
            bookAnimator = book.GetComponent<Animator>();
        else
            Debug.LogWarning("? Book object not found!");

        if (bookcase != null)
            bookcaseAnimator = bookcase.GetComponent<Animator>();
        else
            Debug.LogWarning("? Bookcase object not found!");

        if (txt != null)
            txt.text = "";
    }

    void Update()
    {
        if (isPlayerNear && !hasActivated && Input.GetKeyDown(KeyCode.F))
        {
            hasActivated = true;
            if (txt != null) txt.text = "";

            if (bookAnimator != null)
                bookAnimator.SetTrigger("Pull");

            if (bookcaseAnimator != null)
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
