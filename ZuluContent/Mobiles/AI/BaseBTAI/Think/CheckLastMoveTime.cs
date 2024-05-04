namespace Server.Mobiles;

public class CheckLastMoveTime<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        if (treeState.Creature.LastMoveTime + 1000 < Core.TickCount)
        {
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }
}