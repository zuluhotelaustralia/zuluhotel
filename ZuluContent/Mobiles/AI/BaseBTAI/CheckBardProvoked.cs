namespace Server.Mobiles;

public class CheckBardProvoked<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        if (treeState.Creature.BardProvoked)
        {
            return NodeState.SUCCESS;
        }
        
        return NodeState.FAILURE;
    }
}