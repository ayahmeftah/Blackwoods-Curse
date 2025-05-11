using UnityEngine;

//Script for Mirror4
public class Mirror6Rotation : MonoBehaviour
{
    public float angleRotationY = -12.752f;
    public Transform player;
    public float interactionDistance = 3f;
    public MirrorController mirrorController;

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

        if (isPlayerNearby && Input.GetKeyDown(KeyCode.R) && mirrorController.isFifthMirrorRotated == true)
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

        mirrorController.RotateFifthMirror();
    }

    void OnGUI()
    {
        if (isPlayerNearby && !isRotated)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 26;
            style.fontStyle = FontStyle.Bold;
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.white;

            float width = 400;
            float height = 50;
            float x = (Screen.width - width) / 2;
            float y = (Screen.height - height) / 2;

            GUI.Label(new Rect(x, y, width, height), "Rotate Mirror R", style);
        }
    }
}
