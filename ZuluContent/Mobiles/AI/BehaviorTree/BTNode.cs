using System.Collections.Generic;

namespace Server.Mobiles;

public enum NodeState
{
    RUNNING,
    SUCCESS,
    FAILURE
}

public class BTNode<T> where T : IAIState
{
    public BTNode<T> Parent { get; set; }
    protected List<BTNode<T>> Children { get; } = new();

    public BTNode()
    {
        Parent = null;
    }

    public BTNode(List<BTNode<T>> children)
    {
        foreach (var child in children)
            _Attach(child);
    }

    private void _Attach(BTNode<T> node)
    {
        node.Parent = this;
        Children.Add(node);
    }

    public virtual NodeState Evaluate(T treeState)
    {
        return NodeState.FAILURE;
    }
}