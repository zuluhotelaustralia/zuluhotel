using System;

namespace Server.Mobiles;

public class CheckGuarding<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        var guarding = treeState.IsGuarding;
        var nextStopGuard = treeState.NextStopGuard;

        if (guarding && Core.TickCount < nextStopGuard)
        {
            return NodeState.SUCCESS;
        }
        
        return NodeState.FAILURE;
    }
}