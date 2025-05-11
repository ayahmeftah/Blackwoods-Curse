using UnityEngine;
using UnityEngine.UI;

//Script for Mirror7
public class Mirror3Rotation : MonoBehaviour
{
    public float angleRotationY = 185.076f;
    public Transform player;
    public float interactionDistance = 3f;
    public MirrorController mirrorController;
    public Text txt;

    private bool isPlayerNearby = false;
    private bool isRotated = false;
    private bool canRotate = false; // This flag will control the rotation
    private Quaternion originalRotation;
    private Quaternion targetRotation;

    void Start()
    {
        originalRotation = transform.rotation;
        targetRotation = Quaternion.Euler(transform.eulerAngles.x, angleRotationY, transform.eulerAngles.z);
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        isPlayerNearby = distance <= interactionDistance;

        // Check if the second mirror is rotated and the player is nearby
        if (isPlayerNearby && mirrorController.isSecondMirrorRotated && !canRotate)
        {
            txt.text = "Rotate Mirror3 R";
            canRotate = true; // Allow rotation once this is true
        }

        // Now check for input only if canRotate is true
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.R) && canRotate && !isRotated)
        {
            ToggleRotation();
        }
    }

    void ToggleRotation()
    {
        // If the mirror is already rotated to the target, do nothing
        if (isRotated) return;

        // Rotate to the target position and prevent further toggling
        transform.rotation = targetRotation;
        isRotated = true;

        mirrorController.RotateThirdMirror();
    }
}