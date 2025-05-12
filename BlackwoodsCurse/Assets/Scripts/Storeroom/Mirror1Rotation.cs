using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Mirror1Rotation : MonoBehaviour
{
    public float angleRotationY = 189.27f;
    public Transform player;
    public float interactionDistance = 2.55f;
    public MirrorController mirrorController;
    public Text txt;

    private bool isPlayerNearby = false;
    private bool isRotated = false;
    private Quaternion originalRotation;
    private Quaternion targetRotation;

    private Timer timer;
    private bool isFlashing = false;

    void Start()
    {
        originalRotation = transform.rotation;
        targetRotation = Quaternion.Euler(transform.eulerAngles.x, angleRotationY, transform.eulerAngles.z);

        // Find the Timer instance
        timer = GameObject.FindObjectOfType<Timer>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        isPlayerNearby = distance <= interactionDistance;

        if (isPlayerNearby == true && mirrorController.isFirstMirrorRotated == false)
        {
            txt.text = "Rotate Mirror R\nBreak Mirror B";
        }

        if (isRotated == true || isPlayerNearby ==  false)
        {
            txt.text = ""; // Clear the text
        }

        if (isPlayerNearby == true && Input.GetKeyDown(KeyCode.R) && mirrorController.isFirstMirrorRotated == false)
        {
            ToggleRotation();
        }

        if (isPlayerNearby == true && Input.GetKeyDown(KeyCode.B) && mirrorController.isFirstMirrorRotated == false && !isFlashing)
        {
            ReduceTimer();
        }
    }

    void ToggleRotation()
    {
        if (isRotated) return;

        transform.rotation = targetRotation;
        isRotated = true;
        mirrorController.RotateFirstMirror();
        Debug.Log("Mirror1 Rotated!");
        // Notify the controller that the mirror has fully rotated
        mirrorController.CompleteFirstMirrorRotation();
    }

    void ReduceTimer()
    {
        if (timer != null)
        {
            timer.ReduceTime(3); // Call the reduce time method

            // Flash the timer in red
            StartCoroutine(FlashTimer());
        }
    }

    IEnumerator FlashTimer()
    {
        isFlashing = true;
        timer.HighlightTimer(Color.red); // Change to red
        yield return new WaitForSeconds(1f); // Keep red for 1 second
        timer.HighlightTimer(Color.white); // Revert back to normal
        isFlashing = false;
    }
}
