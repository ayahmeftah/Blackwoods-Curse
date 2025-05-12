using UnityEngine;

public class GrimmArrivesDownstairsTrigger : MonoBehaviour
{
    public GrimmBarrierManager barrierManager;
    public GameObject upstairsFollowTrigger;
    public GameObject downstairsFollowTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Grimm")) return;

        Debug.Log("[GrimmArrivesDownstairsTrigger] ENTERED by: " + other.name);
        GrimmState.isInTransit = false;

        barrierManager.LockGrimmDownstairs();

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