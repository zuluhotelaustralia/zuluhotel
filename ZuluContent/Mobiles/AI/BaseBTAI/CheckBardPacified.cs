namespace Server.Mobiles;

public class CheckBardPacified<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        if (treeState.Creature.BardPacified)
        {
            return NodeState.SUCCESS;
        }
        
        return NodeState.FAILURE;
    }
}