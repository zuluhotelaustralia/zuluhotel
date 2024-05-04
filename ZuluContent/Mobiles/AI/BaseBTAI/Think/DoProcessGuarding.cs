namespace Server.Mobiles;

public class DoProcessGuarding<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        var guarding = treeState.IsGuarding;
        var nextStopGuard = treeState.NextStopGuard;
        
        if (guarding && Core.TickCount < nextStopGuard)
        {
            return NodeState.RUNNING;
        }
        
        return NodeState.SUCCESS;
    }
}