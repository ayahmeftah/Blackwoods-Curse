// Updated GrimmUpstairsFollowTrigger.cs
using UnityEngine;

public class GrimmUpstairsFollowTrigger : MonoBehaviour
{
    public GrimmBehaviorManager grimmManager;
    public GrimmBarrierManager barrierManager;
    public GameObject opposingFollowTrigger;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player")) return;

        Debug.Log("[GrimmUpstairsFollowTrigger] Player entered trigger.");

        if (GrimmState.fungusDialogueActive || GrimmState.isInTransit)
        {
            Debug.Log("[GrimmUpstairsFollowTrigger] Blocked — Fungus or Transit active.");
            return;
        }

        if (opposingFollowTrigger != null)
        {
            Debug.Log("[GrimmUpstairsFollowTrigger] Reactivating opposing trigger: " + opposingFollowTrigger.name);
            opposingFollowTrigger.SetActive(true);
        }

        triggered = true;

        Debug.Log("[GrimmUpstairsFollowTrigger] Disabling barriers and switching Grimm to follow.");
        barrierManager?.DisableBoth();
        grimmManager?.SwitchToFollow();
    }
}