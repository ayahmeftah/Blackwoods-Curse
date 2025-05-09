using UnityEngine;
using Fungus;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public class GrimmFungusTrigger : MonoBehaviour
{
    public Flowchart flowchart;
    public string blockName = "GrimmIntro";

    public BehaviorTree wanderTree; // GrimmCuriousCatBehavior
    public BehaviorTree followTree; // GrimmFollowPlayerBehavior (leave inactive for now)
    public NavMeshAgent navAgent;

    private bool hasStarted = false;

    public void BeginDialogue()
    {
        if (wanderTree != null)
            wanderTree.DisableBehavior();

        if (followTree != null)
            followTree.DisableBehavior(); // Just in case

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
            if (wanderTree != null)
                wanderTree.EnableBehavior(); // Only wander starts after dialogue

            if (navAgent != null)
                navAgent.isStopped = false;

            Destroy(this);
        }
    }
}
