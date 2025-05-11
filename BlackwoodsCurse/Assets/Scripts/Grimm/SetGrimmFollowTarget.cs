using UnityEngine;
using BehaviorDesigner.Runtime;
public class SetGrimmFollowTarget : MonoBehaviour
{
    public BehaviorTree behaviorTree; // Not ExternalBehaviorTree
    public GameObject player;

    void Start()
    {
        if (behaviorTree != null && player != null)
        {
            behaviorTree.SetVariableValue("targetPlayer", player);
            Debug.Log("[Grimm Follow] Assigned player: " + player.name);
        }
        else
        {
            Debug.LogWarning("[Grimm Follow] Missing references.");
        }
    }
}
