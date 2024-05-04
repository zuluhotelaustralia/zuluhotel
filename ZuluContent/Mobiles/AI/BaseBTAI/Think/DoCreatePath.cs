namespace Server.Mobiles;

public class DoCreatePath<T> : BTNode<T> where T : IAIState
{
    private readonly MoveToTargetType m_MoveToTarget;
    
    public DoCreatePath(MoveToTargetType moveToTarget = MoveToTargetType.Combatant)
    {
        m_MoveToTarget = moveToTarget;
    }
    
    private static MoveResult _MoveDirection(Direction d, T treeState)
    {
        return treeState.Creature.DoMoveImpl(d);
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

        if (target == null)
        {
            return NodeState.FAILURE;
        }
        
        treeState.Creature.DebugSay("Creating path to follow to my combatant");
        
        var path = new PathFollower(treeState.Creature, target)
        {
            Mover = (d) => _MoveDirection(d, treeState)
        };
        treeState.Path = path;
        
        return NodeState.SUCCESS;
    }
}