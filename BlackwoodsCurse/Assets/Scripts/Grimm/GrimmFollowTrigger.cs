using UnityEngine;
using BehaviorDesigner.Runtime;

public class GrimmFollowTrigger : MonoBehaviour
{
    [Header("Grimm Setup")]
    public GameObject grimmObject; // Assign Grimm GameObject here

    [Header("Behavior Tree Assets")]
    public ExternalBehaviorTree wanderTreeAsset; // Drag CuriousCat.asset
    public ExternalBehaviorTree followTreeAsset; // Drag FollowPlayer.asset

    private BehaviorTree[] behaviorTrees;
    private bool switched = false;

    void Start()
    {
        if (grimmObject == null)
        {
            Debug.LogError("Grimm GameObject is not assigned!");
            return;
        }

        behaviorTrees = grimmObject.GetComponents<BehaviorTree>();

        if (behaviorTrees.Length == 0)
        {
            Debug.LogError("No BehaviorTree components found on Grimm!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (switched || !other.CompareTag("Player"))
            return;

        Debug.Log("Player entered stairs trigger — switching Grimm behavior...");

        bool wanderDisabled = false;
        bool followEnabled = false;

        foreach (var tree in behaviorTrees)
        {
            if (tree.ExternalBehavior == wanderTreeAsset)
            {
                tree.DisableBehavior();
                Debug.Log("Disabled Wander Tree: " + tree.ExternalBehavior.name);
                wanderDisabled = true;
            }

            if (tree.ExternalBehavior == followTreeAsset)
            {
                tree.EnableBehavior();
                Debug.Log("Enabled Follow Tree: " + tree.ExternalBehavior.name);
                followEnabled = true;
            }
        }

        if (!wanderDisabled)
            Debug.LogWarning("Wander tree not found or not matched!");

        if (!followEnabled)
            Debug.LogWarning("Follow tree not found or not matched!");

        if (followEnabled)
            switched = true;
    }
}
