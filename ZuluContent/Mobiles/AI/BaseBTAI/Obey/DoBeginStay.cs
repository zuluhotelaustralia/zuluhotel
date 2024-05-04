using System;

namespace Server.Mobiles;

public class DoBeginStay<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        if (treeState.LastControlOrder != treeState.Creature.ControlOrder)
        {
            treeState.Creature.Warmode = false;
            treeState.Creature.Combatant = null;
            treeState.Creature.CurrentSpeed = treeState.Creature.PassiveSpeed;

            treeState.Creature.DebugSay("Beginning stay");
            
            treeState.LastControlOrder = treeState.Creature.ControlOrder;
        }

        return NodeState.SUCCESS;
    }
}