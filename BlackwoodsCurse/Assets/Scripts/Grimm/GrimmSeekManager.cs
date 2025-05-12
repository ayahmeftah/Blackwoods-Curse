using UnityEngine;
using BehaviorDesigner.Runtime;

public class GrimmSeekManager : MonoBehaviour
{
    public BehaviorTree behaviorTree;
    public ExternalBehaviorTree seekTree;
    public ExternalBehaviorTree wanderTree;

    public GameObject upstairsTarget;
    public GameObject downstairsTarget;
    public GameObject upstairsWanderArea;
    public GameObject downstairsWanderArea;

    public GameObject upstairsArrivalTrigger;
    public GameObject downstairsArrivalTrigger;

    public void SeekUpstairs()
    {
        behaviorTree.DisableBehavior();
        behaviorTree.ExternalBehavior = seekTree;

        // Safely set global variable
        SharedGameObject go = GlobalVariables.Instance.GetVariable("seekTarget") as SharedGameObject;
        if (go != null)
        {
            go.Value = upstairsTarget;
            Debug.Log("[GrimmSeekManager] Set seekTarget to: " + upstairsTarget.name);
        }
        else
        {
            Debug.LogError("[GrimmSeekManager] Global variable 'seekTarget' is missing or not a GameObject.");
        }

        behaviorTree.EnableBehavior();
        GrimmState.isInTransit = true;
        EnableArrivalTrigger(true);
        Debug.Log("[GrimmSeekManager] Seeking upstairs target.");
    }

    public void SeekDownstairs()
    {
        behaviorTree.DisableBehavior();
        behaviorTree.ExternalBehavior = seekTree;

        SharedGameObject go = GlobalVariables.Instance.GetVariable("seekTarget") as SharedGameObject;
        if (go != null)
        {
            go.Value = downstairsTarget;
            Debug.Log("[GrimmSeekManager] Set seekTarget to: " + downstairsTarget.name);
        }
        else
        {
            Debug.LogError("[GrimmSeekManager] Global variable 'seekTarget' is missing or not a GameObject.");
        }

        behaviorTree.EnableBehavior();
        GrimmState.isInTransit = true;
        EnableArrivalTrigger(false);
        Debug.Log("[GrimmSeekManager] Seeking downstairs target.");
    }


    public void SwitchToWander(bool upstairs)
    {
        behaviorTree.DisableBehavior();
        behaviorTree.ExternalBehavior = wanderTree;
        behaviorTree.EnableBehavior();
        behaviorTree.SetVariableValue("wanderTarget", upstairs ? upstairsWanderArea : downstairsWanderArea);
        GrimmState.isInTransit = false;
        DisableAllArrivalTriggers();
        Debug.Log("[GrimmSeekManager] Switched to wander (" + (upstairs ? "upstairs" : "downstairs") + ")");
    }

    public void EnableArrivalTrigger(bool upstairs)
    {
        if (upstairsArrivalTrigger != null) upstairsArrivalTrigger.SetActive(upstairs);
        if (downstairsArrivalTrigger != null) downstairsArrivalTrigger.SetActive(!upstairs);
    }

    public void DisableAllArrivalTriggers()
    {
        if (upstairsArrivalTrigger != null) upstairsArrivalTrigger.SetActive(false);
        if (downstairsArrivalTrigger != null) downstairsArrivalTrigger.SetActive(false);
    }
}