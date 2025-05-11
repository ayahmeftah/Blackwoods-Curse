using UnityEngine;
using Fungus;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public class GrimmFungusTrigger : MonoBehaviour
{
    public Flowchart flowchart;
    public string blockName = "GrimmIntro";

    public GrimmBehaviorManager grimmManager;
    public NavMeshAgent navAgent;
    public GameObject downstairsArea;

    private bool hasStarted = false;

    public void BeginDialogue()
    {
        if (grimmManager == null || downstairsArea == null)
        {
            Debug.LogError("[GrimmFungusTrigger] Missing grimmManager or downstairsArea.");
            return;
        }

        // Stop Grimm during dialogue
        GrimmState.fungusDialogueActive = true; // set this to block AI

        // Just start the dialogue — do not enable AI yet
        flowchart.ExecuteBlock(blockName);
        hasStarted = true;
    }

    private void Update()
    {
        if (hasStarted && flowchart.GetExecutingBlocks().Count == 0)
        {
            GrimmState.fungusDialogueActive = false; // let AI resume

            if (navAgent != null)
                navAgent.isStopped = false;

            grimmManager.SwitchToWander(downstairsArea); // ONLY this

            Destroy(this); // Fungus done, cleanup
        }
    }
}
