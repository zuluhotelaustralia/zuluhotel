namespace Server.Mobiles;

public class DoClearPath<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        treeState.Path = null;
        
        return NodeState.SUCCESS;
    }
}