using Fungus;
using UnityEngine;

public class TrapdoorEventTrigger : MonoBehaviour
{
    [Header("Fungus")]
    public Flowchart fungusFlowchart;
    public string blockName = "BasementCloseEvent";

    [Header("Exit Trigger")]
    public GameObject exitTriggerObject; // Assign the BasementExitTriggerZone here

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered || !other.CompareTag("Player")) return;
        hasTriggered = true;

        if (fungusFlowchart != null && !string.IsNullOrEmpty(blockName))
        {
            fungusFlowchart.ExecuteBlock(blockName);
        }

        // Enable the exit trigger
        if (exitTriggerObject != null)
        {
            exitTriggerObject.SetActive(true);
        }
    }
}