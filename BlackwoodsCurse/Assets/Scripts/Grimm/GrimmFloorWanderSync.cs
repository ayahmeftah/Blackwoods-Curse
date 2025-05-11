using UnityEngine;
using BehaviorDesigner.Runtime;

public class GrimmFloorWanderSync : MonoBehaviour
{
    public BehaviorTree wanderTree;
    public Transform upstairsTarget;
    public Transform downstairsTarget;

    public void SetWanderTarget(Transform newTarget)
    {
        if (wanderTree != null)
        {
            wanderTree.SetVariableValue("wanderTarget", newTarget);
        }
    }
}