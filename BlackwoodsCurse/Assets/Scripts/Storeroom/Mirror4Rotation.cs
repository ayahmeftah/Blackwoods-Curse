using System.Collections;
using UnityEngine;
using TMPro;

public class Mirror4Rotation : MonoBehaviour
{
    public float angleRotationY = 19.288f;
    public MirrorController mirrorController;
    public TextMeshProUGUI txt;
    [SerializeField] private CanvasGroup textCanvasGroup;

    private bool isPlayerNearby = false;
    private bool isRotated = false;
    private bool canRotate = false;
    private Quaternion originalRotation;
    private Quaternion targetRotation;

    private Timer timer;
    private bool isFlashing = false;

    void Start()
    {
        originalRotation = transform.rotation;
        targetRotation = Quaternion.Euler(transform.eulerAngles.x, angleRotationY, transform.eulerAngles.z);

        timer = GameObject.FindObjectOfType<Timer>();
        textCanvasGroup.alpha = 0f; // Start hidden
    }

    void Update()
    {
        if (isPlayerNearby && mirrorController.isThirdMirrorRotated && !isRotated)
        {
            canRotate = true;

            if (Input.GetKeyDown(KeyCode.R))
            {
                ToggleRotation();
            }

            if (Input.GetKeyDown(KeyCode.B) && !isFlashing)
            {
                ReduceTimer();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && mirrorController.isThirdMirrorRotated && !isRotated)
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
            canRotate = false;
            textCanvasGroup.alpha = 0f;
        }
    }

    void ToggleRotation()
    {
        if (isRotated) return;

        transform.rotation = targetRotation;
        isRotated = true;
        mirrorController.RotateFourthMirror();
        Debug.Log("Mirror4 Rotated!");
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
}
