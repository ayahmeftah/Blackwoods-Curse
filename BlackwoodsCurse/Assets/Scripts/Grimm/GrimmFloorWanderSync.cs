using UnityEngine;
using BehaviorDesigner.Runtime;

public class GrimmFloorWanderSync : MonoBehaviour
{
    public BehaviorTree behaviorTreeComponent;           // The BehaviorTree on Grimm
    public ExternalBehaviorTree wanderTreeAsset;         // Assigned to GrimmWanderTree.asset
    public Transform upstairsTarget;                     // Empty GameObject with cubes upstairs
    public Transform downstairsTarget;                   // Empty GameObject with cubes downstairs

    public void SetWanderTarget(Transform newTarget)
    {
        if (behaviorTreeComponent != null && newTarget != null)
        {
            // DO NOT enable anything here. Just set the target.
            behaviorTreeComponent.SetVariableValue("wanderTarget", newTarget.gameObject);
            Debug.Log("[Grimm] Wander target set to: " + newTarget.name);
        }
        else
        {
            Debug.LogWarning("Missing behaviorTreeComponent, newTarget, or wanderTreeAsset.");
        }
    }
}