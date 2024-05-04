using System;
using System.Collections.Generic;

namespace Server.Mobiles;

public class DoBeginDrop<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        if (!treeState.Creature.CanDrop)
            return NodeState.SUCCESS;

        treeState.Creature.DebugSay("I drop my stuff for my master");

        var pack = treeState.Creature.Backpack;

        if (pack != null)
        {
            List<Item> list = pack.Items;

            for (int i = list.Count - 1; i >= 0; --i)
                if (i < list.Count)
                    list[i].MoveToWorld(treeState.Creature.Location, treeState.Creature.Map);
        }

        treeState.Creature.ControlTarget = null;
        treeState.Creature.ControlOrder = OrderType.None;

        return NodeState.SUCCESS;
    }
}