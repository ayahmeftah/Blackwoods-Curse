using UnityEngine;

public class GrimmDownstairsFollowTrigger : MonoBehaviour
{
    public GrimmBehaviorManager grimmManager;
    public GrimmBarrierManager barrierManager;
    public GameObject opposingFollowTrigger;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player")) return;

        Debug.Log("[GrimmDownstairsFollowTrigger] Player entered trigger.");

        triggered = true;

        if (opposingFollowTrigger != null)
        {
            Debug.Log("[GrimmDownstairsFollowTrigger] Reactivating opposing trigger: " + opposingFollowTrigger.name);
            opposingFollowTrigger.SetActive(true);
        }

        if (GrimmState.fungusDialogueActive || GrimmState.isInTransit)
        {
            Debug.Log("[GrimmUpstairsFollowTrigger] Blocked — Fungus or Transit active.");
            return;
        }


        barrierManager?.DisableBoth();
    }
}