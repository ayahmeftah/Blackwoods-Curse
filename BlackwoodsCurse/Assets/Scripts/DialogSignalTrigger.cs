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

    public void TriggerTruth1()
    {
        flowchart.ExecuteBlock("TruthP1");
    }

    public void TriggerTruth2()
    {
        flowchart.ExecuteBlock("TruthP2");
    }

    public void TriggerTruth3()
    {
        flowchart.ExecuteBlock("TruthP3");
    }

    public void TriggerTruth4()
    {
        flowchart.ExecuteBlock("TruthP4");
    }

    public void TriggerTruth5()
    {
        flowchart.ExecuteBlock("TruthP5");
    }

    public void TriggerTruth6()
    {
        flowchart.ExecuteBlock("TruthP6");
    }

    public void TriggerTruth7()
    {
        flowchart.ExecuteBlock("TruthP7");
    }

    public void TriggerTruth8()
    {
        flowchart.ExecuteBlock("TruthP8");
    }

    public void TriggerTruth9()
    {
        flowchart.ExecuteBlock("TruthP9");
    }

    public void TriggerCastedOutMansion()
    {
        flowchart.ExecuteBlock("CastedOutMansion");
    }

    public void TriggerTrappedScream()
    {
        flowchart.ExecuteBlock("TrappedScream");
    }

    public void TriggerNoEscape()
    {
        flowchart.ExecuteBlock("NoEscape");
    }
}
