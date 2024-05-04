namespace Server.Mobiles;

public class CheckIsDeleted<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        if (treeState.Creature.Deleted)
        {
            return NodeState.SUCCESS;
        }
        
        return NodeState.FAILURE;
    }
}