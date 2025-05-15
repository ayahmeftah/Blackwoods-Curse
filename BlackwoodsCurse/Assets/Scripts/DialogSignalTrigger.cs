using UnityEngine;
using Fungus;

public class DialogSignalTrigger : MonoBehaviour
{
    public Flowchart flowchart;

    public void TriggerApproachLine()
    {
        flowchart.ExecuteBlock("ApproachMansionLine");
    }

    public void TriggerAtMansionLine()
    {
        flowchart.ExecuteBlock("AtMansionLine");
    }

    public void TriggerPulledIntoMansion()
    {
        flowchart.ExecuteBlock("PulledIntoMansion");
    }
}
