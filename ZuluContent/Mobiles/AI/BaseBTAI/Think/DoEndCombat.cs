namespace Server.Mobiles;

public class DoEndCombat<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        treeState.InCombat = false;
        
        return NodeState.SUCCESS;
    }
}