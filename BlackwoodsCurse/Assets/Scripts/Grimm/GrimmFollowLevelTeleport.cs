using UnityEngine;
using UnityEngine.AI;

public class GrimmFollowLevelTeleport : MonoBehaviour
{
    public GameObject grimm;
    public GameObject destination;
    public GameObject wanderArea;
    public NavMeshObstacle upstairsBarrier;
    public NavMeshObstacle downstairsBarrier;
    public bool goUpstairs = true;
    public GameObject opposingTrigger;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player")) return;
        if (GrimmState.fungusDialogueActive || GrimmState.isInTransit)
        {
            Debug.Log("[GrimmFollowLevelTeleport] Blocked due to fungus or transit.");
            return;
        }

        triggered = true;
        GrimmState.isInTransit = true;

        NavMeshAgent agent = grimm.GetComponent<NavMeshAgent>();
        if (agent != null && destination != null)
        {
            agent.Warp(destination.transform.position);
            agent.isStopped = false;
        }

        // Toggle barriers
        if (goUpstairs)
        {
            if (upstairsBarrier != null) upstairsBarrier.enabled = false;
            if (downstairsBarrier != null) downstairsBarrier.enabled = true;
        }
        else
        {
            if (upstairsBarrier != null) upstairsBarrier.enabled = true;
            if (downstairsBarrier != null) downstairsBarrier.enabled = false;
        }

        // Update Behavior Tree wander target
        var behaviorTree = grimm.GetComponent<BehaviorDesigner.Runtime.BehaviorTree>();
        if (behaviorTree != null)
        {
            behaviorTree.SetVariableValue("wanderTarget", wanderArea);
            Debug.Log("[GrimmFollowLevelTeleport] Updated wander target to: " + wanderArea.name);
        }

        if (opposingTrigger != null) opposingTrigger.SetActive(true);

        GrimmState.isInTransit = false;
    }
}