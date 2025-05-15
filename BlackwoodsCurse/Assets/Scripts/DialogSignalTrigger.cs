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

    public void TriggerInsideMansion1()
    {
        flowchart.ExecuteBlock("InsideMansion1");
    }

    public void TriggerInsideMansion2()
    {
        flowchart.ExecuteBlock("InsideMansion2");
    }

    public void TriggerInsideMansion3()
    {
        flowchart.ExecuteBlock("InsideMansion3");
    }

    public void TriggerCastedOut()
    {
        flowchart.ExecuteBlock("CastedOut");
    }
}
