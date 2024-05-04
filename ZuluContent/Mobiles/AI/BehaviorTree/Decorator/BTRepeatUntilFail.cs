using System.Collections.Generic;

namespace Server.Mobiles;

public class BTRepeatUntilFail<T> : BTNode<T> where T : IAIState
{
    public BTRepeatUntilFail()
    {
    }

    public BTRepeatUntilFail(BTNode<T> child) : base(new List<BTNode<T>> { child })
    {
    }

    public override NodeState Evaluate(T treeState)
    {
        var nodeState = NodeState.RUNNING;
        
        while (nodeState != NodeState.FAILURE)
        {
            nodeState = Children[0].Evaluate(treeState);
        }

        return nodeState;
    }
}