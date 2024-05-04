namespace Server.Mobiles;

public class CheckControlChance<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        if (treeState.Creature.CheckControlChance(treeState.Creature.ControlMaster))
        {
            return NodeState.SUCCESS;
        }
        
        return NodeState.FAILURE;
    }
}