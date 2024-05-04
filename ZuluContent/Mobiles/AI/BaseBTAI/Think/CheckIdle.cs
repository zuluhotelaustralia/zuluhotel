namespace Server.Mobiles;

public class CheckIdle<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        if (treeState.Creature.CheckIdle())
        {
            return NodeState.SUCCESS;
        }
        
        return NodeState.FAILURE;
    }
}