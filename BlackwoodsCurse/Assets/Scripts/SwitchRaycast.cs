using UnityEngine;

public class SwitchRaycast : MonoBehaviour
{
    [Header("Raycast Settings")]
    public float raycastDistance = 5f;
    public LayerMask interactableLayer;

    private LightSwitch highlightedSwitch;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
        {
            if (hit.collider.CompareTag("Switch"))
            {
                LightSwitch switchScript = hit.collider.GetComponent<LightSwitch>();

                if (switchScript != null && highlightedSwitch != switchScript)
                {
                    // if (highlightedSwitch != null)
                    //     highlightedSwitch.UnHighlight();

                    // switchScript.Highlight();
                    highlightedSwitch = switchScript;
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    switchScript.ToggleSwitch();
                }
            }
        }
        else
        {
            if (highlightedSwitch != null)
            {
                //highlightedSwitch.UnHighlight();
                highlightedSwitch = null;
            }
        }
    }
}
