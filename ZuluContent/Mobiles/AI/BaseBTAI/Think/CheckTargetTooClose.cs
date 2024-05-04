namespace Server.Mobiles;

public class CheckTargetTooClose<T> : BTNode<T> where T : IAIState
{
    private readonly MoveToTargetType m_MoveToTarget;
    private readonly int m_WantDist;
    
    public CheckTargetTooClose(MoveToTargetType moveToTarget, int wantDist)
    {
        m_MoveToTarget = moveToTarget;
        m_WantDist = wantDist;
    }

    public override NodeState Evaluate(T treeState)
    {
        var target = m_MoveToTarget switch
        {
            MoveToTargetType.ControlTarget => treeState.Creature.ControlTarget,
            MoveToTargetType.ControlMaster => treeState.Creature.ControlMaster,
            _ => treeState.Creature.ControlMaster
        };
        
        var currDist = (int) treeState.Creature.GetDistanceToSqrt(target);

        if (currDist <= m_WantDist)
        {
            return NodeState.SUCCESS;
        }
        
        return NodeState.FAILURE;
    }
}