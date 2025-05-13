using System.Collections;
using UnityEngine;
using TMPro;

public class BreakableMirror : MonoBehaviour
{
    public TextMeshProUGUI txt;
    [SerializeField] private CanvasGroup textCanvasGroup;
    public Material objectMaterial;

    private bool isPlayerNearby = false;
    private bool isBroken = false;

    private Timer timer;
    private bool isFlashing = false;

    void Start()
    {
        // Find the Timer instance
        timer = GameObject.FindObjectOfType<Timer>();

        // Initialize the material if not assigned in the inspector
        if (objectMaterial == null)
        {
            objectMaterial = GetComponent<Renderer>().material;
        }

        // Hide text at start
        textCanvasGroup.alpha = 0f;
    }

    void Update()
    {
        if (isPlayerNearby && !isBroken)
        {
            if (Input.GetKeyDown(KeyCode.R) && !isFlashing)
            {
                ReduceTimer();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                ChangeColorToBlack();
                isBroken = true;
                textCanvasGroup.alpha = 0f; // Hide after breaking
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isBroken)
        {
            isPlayerNearby = true;
            txt.text = "Rotate Mirror R\nBreak Mirror B";
            textCanvasGroup.alpha = 1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            textCanvasGroup.alpha = 0f;
        }
    }

    void ReduceTimer()
    {
        if (timer != null)
        {
            timer.ReduceTime(3);
            StartCoroutine(FlashTimer());
        }
    }

    IEnumerator FlashTimer()
    {
        isFlashing = true;
        timer.HighlightTimer(Color.red);
        yield return new WaitForSeconds(1f);
        timer.HighlightTimer(Color.white);
        isFlashing = false;
    }

    void ChangeColorToBlack()
    {
        if (objectMaterial != null)
        {
            objectMaterial.color = Color.black;
        }
    }
}