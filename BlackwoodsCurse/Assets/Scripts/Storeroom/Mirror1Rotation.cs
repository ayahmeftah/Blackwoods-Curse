using UnityEngine;
using UnityEngine.UI;

public class Mirror1Rotation : MonoBehaviour
{
    public float angleRotationY = 189.27f;
    public Transform player;
    public float interactionDistance = 3f;
    public MirrorController mirrorController;
    public Text txt;

    private bool isPlayerNearby = false;
    private bool isRotated = false;
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

        if (isPlayerNearby == true && mirrorController.isFirstMirrorRotated == false)
        {
            txt.text = "Rotate Mirror1 R";
        }

        if (isPlayerNearby == true && Input.GetKeyDown(KeyCode.R) && mirrorController.isFirstMirrorRotated == false)
        {
            ToggleRotation();
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
}
