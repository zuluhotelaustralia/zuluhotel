using System;

namespace Server.Mobiles;

public class DoBeginAttack<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        if (treeState.LastControlOrder != treeState.Creature.ControlOrder)
        {
            treeState.Creature.Warmode = true;
            treeState.Creature.Combatant = treeState.Creature.ControlTarget;
            treeState.Creature.CurrentSpeed = treeState.Creature.ActiveSpeed;

            treeState.Creature.DebugSay("Beginning attack");
        }

        return NodeState.SUCCESS;
    }
}