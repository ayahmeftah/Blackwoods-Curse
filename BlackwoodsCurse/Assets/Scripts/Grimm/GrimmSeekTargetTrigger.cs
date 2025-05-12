using UnityEngine;

public class GrimmSeekTargetTrigger : MonoBehaviour
{
    public GrimmSeekManager seekManager;
    public bool isUpstairs;

    public GameObject upstairsArrivalTrigger;
    public GameObject downstairsArrivalTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Grimm")) return;

        Debug.Log("[GrimmSeekTargetTrigger] Grimm arrived at target.");

        // Reset transit flag
        GrimmState.isInTransit = false;

        // Disable both triggers to avoid duplicate firing
        if (upstairsArrivalTrigger != null)
            upstairsArrivalTrigger.SetActive(false);

        if (downstairsArrivalTrigger != null)
            downstairsArrivalTrigger.SetActive(false);

        // Switch Grimm to appropriate wander state
        if (seekManager != null)
            seekManager.SwitchToWander(isUpstairs);
    }
}