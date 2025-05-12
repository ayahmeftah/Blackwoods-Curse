using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//Script for Mirror4
public class Mirror6Rotation : MonoBehaviour
{
    public float angleRotationY = -16.045f;
    public Transform player;
    public float interactionDistance = 2.55f;
    public MirrorController mirrorController;
    public Text txt;

    private bool isPlayerNearby = false;
    private bool isRotated = false;
    private bool canRotate = false; // This flag will control the rotation
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

        // Check if the first mirror is rotated and the player is nearby
        if (isPlayerNearby == true && mirrorController.isFifthMirrorRotated && !canRotate)
        {
            txt.text = "Rotate Mirror R\nBreak Mirror B";
            canRotate = true; 
        }
        if (isRotated == true || isPlayerNearby == false)
        {
            txt.text = ""; // Clear the text
        }

        // Now check for input only if canRotate is true
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.R) && canRotate && !isRotated)
        {
            ToggleRotation();
        }

        if (isPlayerNearby == true && Input.GetKeyDown(KeyCode.B) && canRotate && !isRotated && !isFlashing)
        {
            ReduceTimer();
        }
    }

    void ToggleRotation()
    {
        // If the mirror is already rotated to the target, do nothing
        if (isRotated) return;

        // Rotate to the target position and prevent further toggling
        transform.rotation = targetRotation;
        isRotated = true;
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