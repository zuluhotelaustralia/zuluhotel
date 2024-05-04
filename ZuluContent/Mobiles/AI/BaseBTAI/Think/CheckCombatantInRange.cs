namespace Server.Mobiles;

public class CheckCombatantInRange<T> : BTNode<T> where T : IAIState
{
    private readonly int m_Multiplier;
    
    public CheckCombatantInRange(int multiplier = 1)
    {
        m_Multiplier = multiplier;
    }

    public override NodeState Evaluate(T treeState)
    {
        var combatant = treeState.Creature.Combatant;

        if (treeState.Creature.InRange(combatant, treeState.Creature.RangePerception * m_Multiplier))
        {
            return NodeState.SUCCESS;
        }
        
        return NodeState.FAILURE;
    }
}