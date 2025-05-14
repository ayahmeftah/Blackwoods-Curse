using UnityEngine;
using Fungus;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public class GrimmExitDialogueTrigger : MonoBehaviour
{
    public Flowchart flowchart;
    public string dialogueBlockName = "BasementExitDialogue";

    public Transform grimmAppearPoint;               // Where Grimm appears
    public GrimmEventManager grimmEventManager;      // Reference to central manager

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player")) return;
        triggered = true;

        // Warp Grimm to appear point and face forward
        if (grimmEventManager != null && grimmEventManager.grimmObject != null && grimmAppearPoint != null)
        {
            var grimm = grimmEventManager.grimmObject;
            grimm.transform.position = grimmAppearPoint.position;

            Vector3 lookTarget = grimmAppearPoint.forward; // default
            if (grimmEventManager.trapdoorScript != null)
            {
                lookTarget = grimmEventManager.trapdoorScript.transform.position;
            }
            lookTarget.y = grimm.transform.position.y;
            grimm.transform.LookAt(lookTarget);

            // Freeze Grimm while he talks
            var agent = grimm.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.ResetPath();
                agent.isStopped = true;
                agent.updatePosition = false;
                agent.updateRotation = false;
            }

            var rb = grimm.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }

        // Trigger the Fungus dialogue
        if (flowchart != null && !string.IsNullOrEmpty(dialogueBlockName))
        {
            flowchart.ExecuteBlock(dialogueBlockName);
        }
    }
}