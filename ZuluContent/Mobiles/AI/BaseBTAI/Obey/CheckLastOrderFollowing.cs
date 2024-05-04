namespace Server.Mobiles;

public class CheckLastOrderFollowing<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        if (treeState.LastControlOrder is OrderType.Follow or OrderType.Come)
        {
            return NodeState.SUCCESS;
        }
        
        return NodeState.FAILURE;
    }
}