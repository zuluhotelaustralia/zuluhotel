namespace Server.Mobiles;

public class CheckMapIsNullOrInternal<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        if (treeState.Creature.Map == null || treeState.Creature.Map == Map.Internal)
        {
            return NodeState.SUCCESS;
        }
        
        return NodeState.FAILURE;
    }
}