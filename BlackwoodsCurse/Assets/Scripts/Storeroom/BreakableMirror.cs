using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BreakableMirror : MonoBehaviour
{
    public Transform player;
    public float interactionDistance = 2.55f;
    public Text txt;
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
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        isPlayerNearby = distance <= interactionDistance;

        if (isPlayerNearby && isBroken == false)
        {
            txt.text = "Rotate Mirror R\nBreak Mirror B";
        }

        if (isPlayerNearby == true && Input.GetKeyDown(KeyCode.R) && !isFlashing && isBroken == false)
        {
            ReduceTimer();
        }

        // Change to black when "B" is pressed
        if (isPlayerNearby == true && Input.GetKeyDown(KeyCode.B) && isBroken == false)
        {
            ChangeColorToBlack();
            isBroken = true;
        }
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

    void ChangeColorToBlack()
    {
        if (objectMaterial != null)
        {
            objectMaterial.color = Color.black;
        }
    }
}
