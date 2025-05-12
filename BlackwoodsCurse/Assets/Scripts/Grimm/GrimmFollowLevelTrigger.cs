using UnityEngine;
using UnityEngine.AI;

public class GrimmFollowLevelTrigger : MonoBehaviour
{
    public GrimmSeekManager seekManager;
    public GrimmBarrierManager barrierManager;
    public bool goUpstairs = true;

    public GameObject opposingFollowTrigger;
    public GameObject upstairsArrivalTrigger;
    public GameObject downstairsArrivalTrigger;

    public GameObject debugSeekTarget; // Assign the same target as the upstairs/downstairs target

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player")) return;

        if (GrimmState.fungusDialogueActive || GrimmState.isInTransit)
        {
            Debug.Log("[GrimmFollowLevelTrigger] Blocked — fungus or transit active.");
            return;
        }

        triggered = true;

        barrierManager?.DisableBoth();

        if (seekManager != null)
        {
            if (goUpstairs)
            {
                upstairsArrivalTrigger.SetActive(true);
                downstairsArrivalTrigger.SetActive(false);
                seekManager.SeekUpstairs();

                // NavMeshAgent test
                var agent = seekManager.GetComponent<NavMeshAgent>();
                if (agent != null && debugSeekTarget != null)
                {
                    bool success = agent.SetDestination(debugSeekTarget.transform.position);
                    Debug.Log("[GrimmFollowLevelTrigger] TEST: Sent Grimm to debugSeekTarget (Upstairs): " +
                              debugSeekTarget.name + " | Success: " + success);
                }
            }
            else
            {
                downstairsArrivalTrigger.SetActive(true);
                upstairsArrivalTrigger.SetActive(false);
                seekManager.SeekDownstairs();

                var agent = seekManager.GetComponent<NavMeshAgent>();
                if (agent != null && debugSeekTarget != null)
                {
                    bool success = agent.SetDestination(debugSeekTarget.transform.position);
                    Debug.Log("[GrimmFollowLevelTrigger] TEST: Sent Grimm to debugSeekTarget (Downstairs): " +
                              debugSeekTarget.name + " | Success: " + success);
                }
            }
        }

        if (opposingFollowTrigger != null)
        {
            opposingFollowTrigger.SetActive(true);
            Debug.Log("[GrimmFollowLevelTrigger] Enabled opposing trigger: " + opposingFollowTrigger.name);
        }
    }
}
