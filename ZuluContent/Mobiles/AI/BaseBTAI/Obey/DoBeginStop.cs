using System;

namespace Server.Mobiles;

public class DoBeginStop<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        if (treeState.LastControlOrder != treeState.Creature.ControlOrder)
        {
            treeState.Creature.Home =  treeState.Creature.Location;
            treeState.Creature.Warmode = false;
            treeState.Creature.Combatant = null;
            treeState.Creature.CurrentSpeed = treeState.Creature.PassiveSpeed;

            treeState.Creature.DebugSay("Beginning stop");

            treeState.LastControlOrder = treeState.Creature.ControlOrder;
        }

        return NodeState.SUCCESS;
    }
}