using UnityEngine;
using Fungus;
using UnityEngine.AI;

public class GrimmFungusTrigger : MonoBehaviour
{
    public Flowchart flowchart;
    public string blockName = "GrimmIntro";

    public GrimmSeekManager seekManager;
    public NavMeshAgent navAgent;

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

    void Update()
    {
        if (!hasStarted || resumed) return;

        if (flowchart != null && flowchart.GetExecutingBlocks().Count == 0)
        {
            GrimmState.fungusDialogueActive = false;
            resumed = true;

            if (navAgent != null)
                navAgent.isStopped = false;

            if (seekManager != null)
                seekManager.SwitchToWander(false); // wander downstairs by default

            if (upstairsTriggerToEnable != null)
                upstairsTriggerToEnable.SetActive(true);

            if (downstairsTriggerToDisable != null)
                downstairsTriggerToDisable.SetActive(false);

            Debug.Log("[GrimmFungusTrigger] Dialogue done. AI resumed. Upstairs trigger enabled.");
        }
    }
}
