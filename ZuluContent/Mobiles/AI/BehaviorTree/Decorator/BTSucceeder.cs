using System.Collections.Generic;

namespace Server.Mobiles;

public class BTSucceeder<T> : BTNode<T> where T : IAIState
{
    public BTSucceeder()
    {
    }
    
    public BTSucceeder(BTNode<T> child) : base(new List<BTNode<T>> { child }) { }

    public override NodeState Evaluate(T treeState)
    {
        var nodeState = Children[0].Evaluate(treeState);

        return nodeState == NodeState.RUNNING ? NodeState.RUNNING : NodeState.SUCCESS;
    }
}