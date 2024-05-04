namespace Server.Mobiles;

public class CheckTargetTooFarAway<T> : BTNode<T> where T : IAIState
{
    private readonly MoveToTargetType m_MoveToTarget;

    public CheckTargetTooFarAway(MoveToTargetType moveToTarget = MoveToTargetType.Combatant)
    {
        m_MoveToTarget = moveToTarget;
    }
    
    public override NodeState Evaluate(T treeState)
    {
        var target = m_MoveToTarget switch
        {
            MoveToTargetType.Combatant => treeState.Creature.Combatant,
            MoveToTargetType.ControlTarget => treeState.Creature.ControlTarget,
            MoveToTargetType.ControlMaster => treeState.Creature.ControlMaster,
            _ => treeState.Creature.Combatant
        };

        if (treeState.Creature.GetDistanceToSqrt(target) > treeState.Creature.RangePerception + 1)
        {
            treeState.Creature.DebugSay("My target is too far away");
            
            return NodeState.SUCCESS;
        }
        
        return NodeState.FAILURE;
    }
}