using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class GrimmSeekManager : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject upstairsArrivalTarget;      // Empty GameObject at arrival point
    public GameObject downstairsArrivalTarget;    // Same for downstairs

    public GameObject upstairsWanderArea;
    public GameObject downstairsWanderArea;

    public BehaviorTree behaviorTree;
    public ExternalBehaviorTree wanderTree;

    public void SeekUpstairs()
    {
        if (agent == null || upstairsArrivalTarget == null)
        {
            Debug.LogError("[GrimmSeekManager] Missing NavMeshAgent or upstairsArrivalTarget.");
            return;
        }

        agent.SetDestination(upstairsArrivalTarget.transform.position);
        GrimmState.isInTransit = true;
        Debug.Log("[GrimmSeekManager] Moving Grimm upstairs to: " + upstairsArrivalTarget.name);
    }

    public void SeekDownstairs()
    {
        if (agent == null || downstairsArrivalTarget == null)
        {
            Debug.LogError("[GrimmSeekManager] Missing NavMeshAgent or downstairsArrivalTarget.");
            return;
        }

        agent.SetDestination(downstairsArrivalTarget.transform.position);
        GrimmState.isInTransit = true;
        Debug.Log("[GrimmSeekManager] Moving Grimm downstairs to: " + downstairsArrivalTarget.name);
    }

    public void SwitchToWander(bool upstairs)
    {
        if (behaviorTree == null || wanderTree == null) return;

        behaviorTree.DisableBehavior();
        behaviorTree.ExternalBehavior = wanderTree;
        behaviorTree.EnableBehavior();

        GameObject area = upstairs ? upstairsWanderArea : downstairsWanderArea;
        behaviorTree.SetVariableValue("wanderTarget", area);

        GrimmState.isInTransit = false;
        Debug.Log("[GrimmSeekManager] Switched to wander mode. Area: " + area.name);
    }

    public void TestDirectMove(GameObject target)
    {
        var agent = GetComponent<NavMeshAgent>();
        if (agent == null || target == null)
        {
            Debug.LogError("[GrimmSeekManager] NavMeshAgent or target is null.");
            return;
        }

        bool success = agent.SetDestination(target.transform.position);
        Debug.Log("[GrimmSeekManager] Direct SetDestination to: " + target.name + " | success: " + success);
    }

}
