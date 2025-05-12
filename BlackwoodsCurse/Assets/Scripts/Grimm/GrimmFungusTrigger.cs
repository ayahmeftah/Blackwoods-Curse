using UnityEngine;
using Fungus;
using UnityEngine.AI;

public class GrimmFungusTrigger : MonoBehaviour
{
    public Flowchart flowchart;
    public string blockName = "GrimmIntro";

    public NavMeshAgent navAgent;
    public GameObject downstairsWanderArea;
    public GameObject upstairsTriggerToEnable;
    public GameObject downstairsTriggerToDisable;

    private bool hasStarted = false;
    private bool resumed = false;

    public void BeginDialogue()
    {
        GrimmState.fungusDialogueActive = true;

        if (navAgent != null)
        {
            navAgent.isStopped = true;
            navAgent.velocity = Vector3.zero;
        }

        if (flowchart != null)
        {
            flowchart.ExecuteBlock(blockName);
            hasStarted = true;
        }
    }

    private void Update()
    {
        if (!hasStarted || resumed) return;

        if (flowchart != null && flowchart.GetExecutingBlocks().Count == 0)
        {
            GrimmState.fungusDialogueActive = false;
            resumed = true;

            if (navAgent != null && downstairsWanderArea != null)
            {
                navAgent.Warp(downstairsWanderArea.transform.position);
                navAgent.isStopped = false;
            }

            // Assign wander behavior
            var tree = navAgent.GetComponent<BehaviorDesigner.Runtime.BehaviorTree>();
            if (tree != null)
            {
                tree.DisableBehavior();
                tree.EnableBehavior();
                tree.SetVariableValue("wanderTarget", downstairsWanderArea);
                Debug.Log("[GrimmFungusTrigger] Wander target set to: " + downstairsWanderArea.name);
            }

            if (upstairsTriggerToEnable != null)
                upstairsTriggerToEnable.SetActive(true);

            if (downstairsTriggerToDisable != null)
                downstairsTriggerToDisable.SetActive(false);

            Debug.Log("[GrimmFungusTrigger] Dialogue done. Grimm warped and AI resumed.");
            Destroy(this);
        }
    }
}