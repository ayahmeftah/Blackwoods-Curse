using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class GrimmEventManager : MonoBehaviour
{
    public GameObject grimmObject;
    public Transform bookcasePoint;   // Assigned in Inspector
    public Transform bookTransform;   // The book he should look at
    public ExternalBehaviorTree investigateTree;
    public ExternalBehaviorTree wanderTree;
    public ExternalBehaviorTree codeHintTree;
    public GameObject hintFlowchart;

    public void StartBookcaseInvestigation()
    {

        Debug.Log("Grimm investigation triggered.");

        // Position Grimm
        grimmObject.transform.position = bookcasePoint.position;

        // Rotate Grimm to face book
        Vector3 lookTarget = bookTransform.position;
        lookTarget.y = grimmObject.transform.position.y;
        grimmObject.transform.LookAt(lookTarget);

        // Stop NavMeshAgent movement
        var agent = grimmObject.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.ResetPath();
            agent.isStopped = true;
            agent.updatePosition = false;
            agent.updateRotation = false;
        }

        // Freeze all Rigidbody movement
        var rb = grimmObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        var bt = grimmObject.GetComponent<BehaviorTree>();
        if (bt == null)
        {
            Debug.LogError("BehaviorTree component not found on Grimm!");
            return;
        }

        bt.DisableBehavior();                      // stop current tree
        bt.ExternalBehavior = investigateTree;     // switch to new one
        bt.SetVariableValue("grimmFungusTriggerObject", grimmObject);
        bt.SetVariableValue("eventManagerObject", this.gameObject);
        bt.SetVariableValue("flowchartObject", hintFlowchart);
        bt.EnableBehavior();                       // start new one

        Debug.Log("InvestigateBookcase tree enabled.");
    }


    public void OnHintDialogueComplete()
    {
        Debug.Log("OnHintDialogueComplete CALLED!");
        GrimmState.fungusDialogueActive = false;
    }

    public void ResumeWandering()
    {
        Debug.Log("Grimm resuming wandering...");

        // Unfreeze Rigidbody
        var rb = grimmObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation; // allow movement
        }

        // Resume NavMeshAgent control
        var agent = grimmObject.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.isStopped = false;
            agent.updatePosition = true;
            agent.updateRotation = true;
        }

        var bt = grimmObject.GetComponent<BehaviorTree>();
        bt.DisableBehavior();
        bt.ExternalBehavior = wanderTree;
        bt.EnableBehavior();
    }

    public void StartCodeHintDialogue(string blockName)
    {
        Debug.Log("Grimm summoned for code hint.");

        grimmObject.transform.position = bookcasePoint.position;

        Vector3 lookTarget = bookTransform.position;
        lookTarget.y = grimmObject.transform.position.y;
        grimmObject.transform.LookAt(lookTarget);

        var agent = grimmObject.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.ResetPath();
            agent.isStopped = true;
            agent.updatePosition = false;
            agent.updateRotation = false;
        }

        var rb = grimmObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        var bt = grimmObject.GetComponent<BehaviorTree>();
        if (bt == null)
        {
            Debug.LogError("BehaviorTree component not found on Grimm!");
            return;
        }

        bt.DisableBehavior();
        bt.ExternalBehavior = codeHintTree; // switch to dedicated hint tree
        bt.SetVariableValue("grimmFungusTriggerObject", grimmObject);
        bt.SetVariableValue("eventManagerObject", this.gameObject);
        bt.SetVariableValue("flowchartObject", hintFlowchart);
        //bt.SetVariableValue("blockName", blockName); // If using a dynamic name
        bt.EnableBehavior();

        Debug.Log("Code hint behavior tree enabled.");
    }

}