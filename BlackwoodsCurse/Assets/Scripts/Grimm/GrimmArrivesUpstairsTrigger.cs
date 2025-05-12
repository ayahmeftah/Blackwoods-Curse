using UnityEngine;

public class GrimmArrivesUpstairsTrigger : MonoBehaviour
{
    public GrimmBarrierManager barrierManager;
    public GameObject downstairsFollowTrigger;
    public GameObject upstairsFollowTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Grimm")) return;

        Debug.Log("[GrimmArrivesUpstairsTrigger] ENTERED by: " + other.name);
        GrimmState.isInTransit = false;

        barrierManager.LockGrimmUpstairs();

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