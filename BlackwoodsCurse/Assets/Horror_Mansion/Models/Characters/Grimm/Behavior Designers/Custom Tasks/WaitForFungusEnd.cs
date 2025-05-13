using BehaviorDesigner.Runtime.Tasks;

public class WaitForFungusEnd : Conditional
{
    public override TaskStatus OnUpdate()
    {
        return GrimmState.fungusDialogueActive ? TaskStatus.Running : TaskStatus.Success;
    }
}
