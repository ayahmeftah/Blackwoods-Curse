using UnityEngine;
using TMPro;

public class PouringZone : MonoBehaviour
{
    public FlaskMixingManager mixingManager;
    public TextMeshProUGUI interactionText;

    private bool isPlayerNearby = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactionText.text = "Pour Red R\nPour Blue B\nPour Yellow Y";
            interactionText.alpha = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactionText.alpha = 0;
        }
    }

    private void Update()
    {
        if (!isPlayerNearby || mixingManager.IsPouring) return;

        if (Input.GetKeyDown(KeyCode.B))
        {
            mixingManager.PourFlaskByColor("blue");
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            mixingManager.PourFlaskByColor("yellow");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            mixingManager.PourFlaskByColor("red");
        }
    }
}
