namespace Server.Mobiles;

public class DoMoveToTarget<T> : BTNode<T> where T : IAIState
{
    private readonly MoveToTargetType m_MoveToTarget;
    private readonly bool m_BadStateOk;
    private readonly bool m_InvertMovement;

    public DoMoveToTarget(MoveToTargetType moveToTarget, bool badStateOk, bool invertMovement = false)
    {
        m_MoveToTarget = moveToTarget;
        m_BadStateOk = badStateOk;
        m_InvertMovement = invertMovement;
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

        treeState.Creature.DebugSay("Trying to move to my target");

        var dirTo = m_InvertMovement
            ? target.GetDirectionTo(treeState.Creature)
            : treeState.Creature.GetDirectionTo(target);

        var res = treeState.Creature.DoMoveImpl(dirTo);

        var success = res == MoveResult.Success || res == MoveResult.SuccessAutoTurn ||
                      m_BadStateOk && res == MoveResult.BadState;

        if (success) return NodeState.SUCCESS;

        return NodeState.FAILURE;
    }
}