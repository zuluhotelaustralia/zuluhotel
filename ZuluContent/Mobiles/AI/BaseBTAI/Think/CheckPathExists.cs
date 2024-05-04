namespace Server.Mobiles;

public class CheckPathExists<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        var combatant = treeState.Creature.Combatant;

        var path = treeState.Path;

        if (path != null && path.Goal == combatant)
        {
            return NodeState.SUCCESS;
        }
        
        return NodeState.FAILURE;
    }
}