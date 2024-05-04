using System.Collections.Generic;

namespace Server.Mobiles;

public class BTSelector<T> : BTNode<T> where T : IAIState
{
    public BTSelector()
    {
    }

    public BTSelector(List<BTNode<T>> children) : base(children)
    {
    }

    public override NodeState Evaluate(T treeState)
    {
        foreach (var node in Children)
            switch (node.Evaluate(treeState))
            {
                case NodeState.FAILURE:
                    continue;
                case NodeState.SUCCESS:
                    return NodeState.SUCCESS;
                case NodeState.RUNNING:
                    return NodeState.RUNNING;
                default:
                    continue;
            }
        
        return NodeState.FAILURE;
    }
}