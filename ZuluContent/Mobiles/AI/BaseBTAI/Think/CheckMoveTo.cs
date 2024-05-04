namespace Server.Mobiles;

public enum MoveToTargetType
{
    Combatant,
    ControlTarget,
    ControlMaster
}

public class CheckMoveTo<T> : BTNode<T> where T : IAIState
{
    private readonly MoveToTargetType m_MoveToTarget;

    public CheckMoveTo(MoveToTargetType moveToTarget = MoveToTargetType.Combatant)
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

        if (treeState.Creature.Deleted || treeState.Creature.DisallowAllMoves || target == null || target.Deleted)
            return NodeState.FAILURE;

        return NodeState.SUCCESS;
    }
}