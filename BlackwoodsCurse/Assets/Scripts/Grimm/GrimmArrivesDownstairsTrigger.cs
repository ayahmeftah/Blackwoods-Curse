using UnityEngine;

public class GrimmArrivesDownstairsTrigger : MonoBehaviour
{
    public GrimmBehaviorManager grimmManager;
    public GrimmBarrierManager barrierManager;
    public GameObject downstairsArea;

    public GameObject upstairsFollowTrigger;
    public GameObject downstairsFollowTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Grimm")) return;

        Debug.Log("[GrimmArrivesDownstairsTrigger] ENTERED by: " + other.name);

        if (!GrimmState.fungusDialogueActive)
        {
            grimmManager.SwitchToWander(downstairsArea);
        }

        GrimmState.isInTransit = false; // Grimm finished his move

        barrierManager?.LockGrimmDownstairs();

        if (upstairsFollowTrigger != null)
        {
            upstairsFollowTrigger.SetActive(true);
            Debug.Log("[GrimmArrivesDownstairsTrigger] Enabled upstairs follow trigger.");
        }

        if (downstairsFollowTrigger != null)
        {
            downstairsFollowTrigger.SetActive(false);
            Debug.Log("[GrimmArrivesDownstairsTrigger] Disabled downstairs follow trigger.");
        }
    }
}