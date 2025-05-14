using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class ResumeWandering : Action
{
    public SharedGameObject eventManagerObject;

    public override TaskStatus OnUpdate()
    {
        if (eventManagerObject == null || eventManagerObject.Value == null)
        {
            Debug.LogError("eventManagerObject is not assigned.");
            return TaskStatus.Failure;
        }

        var manager = eventManagerObject.Value.GetComponent<GrimmEventManager>();
        if (manager != null)
        {
            manager.ResumeWandering();
            return TaskStatus.Success;
        }

        Debug.LogError("GrimmEventManager component not found.");
        return TaskStatus.Failure;
    }
}
