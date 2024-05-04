using System.Collections.Generic;

namespace Server.Mobiles;

public class BTSequence<T> : BTNode<T> where T : IAIState
{
    public BTSequence()
    {
    }

    public BTSequence(List<BTNode<T>> children) : base(children)
    {
    }

    public override NodeState Evaluate(T treeState)
    {
        foreach (var node in Children)
            switch (node.Evaluate(treeState))
            {
                case NodeState.SUCCESS:
                    continue;
                case NodeState.FAILURE:
                    return NodeState.FAILURE;
                case NodeState.RUNNING:
                    return NodeState.RUNNING;
                default:
                    return NodeState.SUCCESS;
            }
        
        return NodeState.SUCCESS;
    }
}