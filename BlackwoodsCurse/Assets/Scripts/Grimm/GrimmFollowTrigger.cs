using UnityEngine;

public class GrimmFollowTrigger : MonoBehaviour
{
    public GrimmBehaviorManager grimmManager;       // Assign Grimm's behavior manager
    public GameObject stairsBarrier;                // Assign GrimmStairsBarrier

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player")) return;

        triggered = true;

        if (stairsBarrier != null)
        {
            stairsBarrier.SetActive(false); // Remove stair block
            Debug.Log("[Grimm] Barrier disabled upstairs.");
        }

        if (grimmManager != null)
        {
            grimmManager.SwitchToFollow(); // Grimm switches to follow the player
        }
    }
}