using BehaviorDesigner.Runtime.Tasks;

public class IsBookcaseMoved : Conditional
{
    public override TaskStatus OnUpdate()
    {
        return GrimmState.bookcaseMoved ? TaskStatus.Success : TaskStatus.Failure;
    }
}
