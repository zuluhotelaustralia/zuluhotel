namespace Server.Mobiles;

public class CheckControlled<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        if (treeState.Creature.Controlled)
        {
            return NodeState.SUCCESS;
        }
        
        return NodeState.FAILURE;
    }
}