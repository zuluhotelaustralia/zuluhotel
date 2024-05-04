using System;

namespace Server.Mobiles;

public class DoBeginFollow<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        if (treeState.LastControlOrder != treeState.Creature.ControlOrder)
        {
            treeState.Creature.Warmode = false;
            treeState.Creature.Combatant = null;
            treeState.Creature.CurrentSpeed = 0.01;

            treeState.Creature.DebugSay("Beginning follow");
            
            treeState.LastControlOrder = treeState.Creature.ControlOrder;
        }

        return NodeState.SUCCESS;
    }
}