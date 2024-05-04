namespace Server.Mobiles;

public class CheckCombatantInAttackRange<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        var combatant = treeState.Creature.Combatant;

        if (treeState.Creature.InRange(combatant, treeState.Creature.RangeFight))
        {
            return NodeState.SUCCESS;
        }
        
        return NodeState.FAILURE;
    }
}