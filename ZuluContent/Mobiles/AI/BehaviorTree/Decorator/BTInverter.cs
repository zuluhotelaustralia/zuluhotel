using System.Collections.Generic;

namespace Server.Mobiles;

public class BTInverter<T> : BTNode<T> where T : IAIState
{
    public BTInverter()
    {
    }
    
    public BTInverter(BTNode<T> child) : base(new List<BTNode<T>> { child }) { }

    public override NodeState Evaluate(T treeState)
    {
        var nodeState = Children[0].Evaluate(treeState);

        return nodeState switch
        {
            NodeState.FAILURE => NodeState.SUCCESS,
            NodeState.SUCCESS => NodeState.FAILURE,
            _ => nodeState
        };
    }
}