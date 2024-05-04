using System;

namespace Server.Mobiles;

public class DoBeginCome<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        if (treeState.LastControlOrder != treeState.Creature.ControlOrder)
        {
            treeState.Creature.Warmode = false;
            treeState.Creature.Combatant = null;
            treeState.Creature.CurrentSpeed = treeState.Creature.ActiveSpeed;

            treeState.Creature.DebugSay("Beginning come");
            
            treeState.LastControlOrder = treeState.Creature.ControlOrder;
        }

        return NodeState.SUCCESS;
    }
}