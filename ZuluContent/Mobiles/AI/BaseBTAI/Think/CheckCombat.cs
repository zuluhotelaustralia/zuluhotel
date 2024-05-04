namespace Server.Mobiles;

public class CheckCombat<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        if (treeState.InCombat)
        {
            return NodeState.SUCCESS;
        }
        
        return NodeState.FAILURE;
    }
}