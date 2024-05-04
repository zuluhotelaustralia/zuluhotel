using System;

namespace Server.Mobiles;

public class DoEndGuarding<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        treeState.IsGuarding = false;
        
        return NodeState.SUCCESS;
    }
}