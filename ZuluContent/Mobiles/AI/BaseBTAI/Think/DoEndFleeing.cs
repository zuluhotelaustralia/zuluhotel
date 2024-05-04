using System;

namespace Server.Mobiles;

public class DoEndFleeing<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        treeState.IsFleeing = false;
        
        return NodeState.SUCCESS;
    }
}