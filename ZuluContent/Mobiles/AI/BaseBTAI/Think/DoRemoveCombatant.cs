namespace Server.Mobiles;

public class DoRemoveCombatant<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        treeState.Creature.Combatant = null;
        
        return NodeState.SUCCESS;
    }
}