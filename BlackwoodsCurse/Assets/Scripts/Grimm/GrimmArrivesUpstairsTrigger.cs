using UnityEngine;

public class GrimmArrivesUpstairsTrigger : MonoBehaviour
{
    public GrimmBehaviorManager grimmManager;
    public GrimmBarrierManager barrierManager;
    public GameObject upstairsArea;

    public GameObject downstairsFollowTrigger;
    public GameObject upstairsFollowTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Grimm")) return;

        Debug.Log("[GrimmArrivesUpstairsTrigger] ENTERED by: " + other.name);

        if (!GrimmState.fungusDialogueActive)
        {
            grimmManager.SwitchToWander(upstairsArea);
        }

        GrimmState.isInTransit = false; // Grimm finished his move

        barrierManager?.LockGrimmUpstairs();

        if (downstairsFollowTrigger != null)
        {
            downstairsFollowTrigger.SetActive(true);
            Debug.Log("[GrimmArrivesUpstairsTrigger] Enabled downstairs follow trigger.");
        }

        if (upstairsFollowTrigger != null)
        {
            upstairsFollowTrigger.SetActive(false);
            Debug.Log("[GrimmArrivesUpstairsTrigger] Disabled upstairs follow trigger.");
        }
    }
}