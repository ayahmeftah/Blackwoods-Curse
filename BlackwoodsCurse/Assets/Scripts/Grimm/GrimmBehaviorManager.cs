using UnityEngine;
using BehaviorDesigner.Runtime;

public class GrimmBehaviorManager : MonoBehaviour
{
    public BehaviorTree behaviorTree;               // The shared BehaviorTree on Grimm
    public ExternalBehaviorTree wanderTreeAsset;
    public ExternalBehaviorTree followTreeAsset;

    public GameObject player;
    public GameObject upstairsArea;
    public GameObject downstairsArea;

    public void SwitchToWander(GameObject area)
    {
        if (behaviorTree == null || wanderTreeAsset == null || area == null)
        {
            Debug.LogError("[GrimmBehaviorManager] Missing references in SwitchToWander.");
            return;
        }

        Debug.Log("[GrimmBehaviorManager] Switching to WANDER at: " + area.name);

        behaviorTree.DisableBehavior();
        behaviorTree.ExternalBehavior = wanderTreeAsset;
        behaviorTree.EnableBehavior();

        behaviorTree.SetVariableValue("wanderTarget", area);
        Debug.Log("[GrimmBehaviorManager] SetVariable wanderTarget");
    }

    public void SwitchToFollow()
    {
        if (behaviorTree == null || followTreeAsset == null || player == null)
        {
            Debug.LogError("[GrimmBehaviorManager] Missing references in SwitchToFollow.");
            return;
        }

        GrimmState.isInTransit = true;
        Debug.Log("[GrimmBehaviorManager] Switching to FOLLOW. Transit=true.");

        behaviorTree.DisableBehavior();
        behaviorTree.ExternalBehavior = followTreeAsset;
        behaviorTree.EnableBehavior();

        behaviorTree.SetVariableValue("targetPlayer", player);
        Debug.Log("[GrimmBehaviorManager] SetVariable targetPlayer");
    }
}
