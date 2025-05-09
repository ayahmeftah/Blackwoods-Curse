using UnityEngine;

public class MirrorRotation : MonoBehaviour
{
    public float rotationAngle = 90f;        
    public Transform player;                
    public float interactionDistance = 3f;  

    private bool isPlayerNearby = false;
    private bool isRotated = false;
    private Quaternion originalRotation;
    private Quaternion targetRotation;

    void Start()
    {
        originalRotation = transform.rotation;
        targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, rotationAngle, 0));
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        isPlayerNearby = distance <= interactionDistance;

        if (isPlayerNearby && Input.GetKeyDown(KeyCode.R))
        {
            ToggleRotation();
        }
    }

    void ToggleRotation()
    {
        if (isRotated)
            transform.rotation = originalRotation;
        else
            transform.rotation = targetRotation;

        isRotated = !isRotated;
    }

    void OnGUI()
    {
        if (isPlayerNearby)
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
