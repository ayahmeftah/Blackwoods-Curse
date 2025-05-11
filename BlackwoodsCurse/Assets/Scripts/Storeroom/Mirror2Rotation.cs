using UnityEngine;
using UnityEngine.UI;

public class Mirror2Rotation : MonoBehaviour
{
    public float angleRotationY = 6.36f;
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

        if (isPlayerNearby && mirrorController.isFirstMirrorFullyRotated && !isRotated)
        {
            txt.text = "Rotate Mirror2 R";
        }

        if (isPlayerNearby && Input.GetKeyDown(KeyCode.R) && mirrorController.isFirstMirrorFullyRotated && !isRotated)
        {
            ToggleRotation();
        }
    }

    void ToggleRotation()
    {
        if (isRotated) return;

        transform.rotation = targetRotation;
        isRotated = true;
        mirrorController.RotateSecondMirror();
        Debug.Log("Mirror2 Rotated!");
    }
}
