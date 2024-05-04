using System;

namespace Server.Mobiles;

public class DoChangeLastOrder<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        treeState.Creature.ControlOrder = treeState.LastControlOrder;

        if (treeState.Creature.ControlOrder is OrderType.Follow or OrderType.Come)
            treeState.Creature.ControlTarget = treeState.Creature.ControlMaster;

        return NodeState.SUCCESS;
    }
}