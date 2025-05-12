using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using Fungus;

public class TriggerFungusDialogue : Action
{
    public SharedGameObject flowchartObject;   // New shared GameObject for the Flowchart
    public string blockName = "GrimmBookcaseHint"; // Name of the dialogue block to trigger

    private Flowchart flowchart;

    public override void OnStart()
    {
        if (flowchartObject != null && flowchartObject.Value != null)
        {
            flowchart = flowchartObject.Value.GetComponent<Flowchart>();
        }

        if (flowchart != null)
        {
            GrimmState.fungusDialogueActive = true;
            GrimmState.hasGivenBookHint = true;
            flowchart.ExecuteBlock(blockName);
        }
        else
        {
            Debug.LogError("Flowchart not found or not assigned.");
        }
    }

    public void BeginDialogue()
    {
        if (flowchart != null)
        {
            Debug.Log(">> Starting Grimm hint dialogue...");
            GrimmState.fungusDialogueActive = true;
            flowchart.ExecuteBlock(blockName);
        }
        else
        {
            Debug.LogError("Flowchart not assigned!");
        }
    }


    public override TaskStatus OnUpdate()
    {
        Debug.Log($"[WaitForFungusEnd] fungusDialogueActive = {GrimmState.fungusDialogueActive}");
        return GrimmState.fungusDialogueActive ? TaskStatus.Running : TaskStatus.Success;
    }
}
