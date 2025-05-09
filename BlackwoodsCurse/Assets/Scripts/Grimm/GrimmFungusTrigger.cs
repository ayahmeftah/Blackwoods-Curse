using UnityEngine;
using Fungus;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public class GrimmFungusTrigger : MonoBehaviour
{
    public Flowchart flowchart;
    public string blockName = "GrimmIntro";

    [Header("Grimm Setup")]
    public GameObject grimmObject;                 
    public ExternalBehaviorTree wanderTreeAsset;    
    public NavMeshAgent navAgent;

    private BehaviorTree wanderTree;
    private bool hasStarted = false;

    public void BeginDialogue()
    {
        if (grimmObject == null || wanderTreeAsset == null)
        {
            Debug.LogError("Missing Grimm GameObject or wanderTreeAsset!");
            return;
        }

        BehaviorTree[] allTrees = grimmObject.GetComponents<BehaviorTree>();
        foreach (var tree in allTrees)
        {
            if (tree.ExternalBehavior == wanderTreeAsset)
            {
                wanderTree = tree;
                break;
            }
        }

        if (wanderTree == null)
        {
            Debug.LogError("Wander BehaviorTree not found on Grimm!");
            return;
        }

        wanderTree.DisableBehavior();

        if (navAgent != null)
        {
            navAgent.isStopped = true;
            navAgent.velocity = Vector3.zero;
        }

        flowchart.ExecuteBlock(blockName);
        hasStarted = true;
    }

    void Update()
    {
        if (hasStarted && flowchart.GetExecutingBlocks().Count == 0)
        {
            Debug.Log("Enabling wander tree now");
            wanderTree.EnableBehavior();

            if (navAgent != null)
                navAgent.isStopped = false;

            Destroy(this);
        }
    }
}
