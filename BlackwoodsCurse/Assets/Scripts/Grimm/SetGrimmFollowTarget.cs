using UnityEngine;
using BehaviorDesigner.Runtime;

public class SetGrimmFollowTarget : MonoBehaviour
{
    public BehaviorTree followTree; // Assign Grimm's FollowPlayer behavior tree
    public GameObject player;       // Drag your Player GameObject here

    void Start()
    {
        if (followTree != null && player != null)
        {
            followTree.SetVariableValue("targetPlayer", player);
            Debug.Log("Assigned player to Grimm's follow target.");
        }
        else
        {
            Debug.LogWarning("Missing followTree or player reference.");
        }
    }
}