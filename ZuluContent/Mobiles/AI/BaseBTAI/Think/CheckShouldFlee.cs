using System;

namespace Server.Mobiles;

public class CheckShouldFlee<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        if (!treeState.Creature.Controlled && !treeState.Creature.Summoned && treeState.Creature.CanFlee)
            if (treeState.Creature.Hits < treeState.Creature.HitsMax * 20 / 100)
            {
                // We are low on health, should we flee?
                var flee = false;

                if (treeState.Creature.Hits < treeState.Creature.Combatant.Hits)
                {
                    // We are more hurt than them
                    var diff = treeState.Creature.Combatant.Hits - treeState.Creature.Hits;

                    flee = Utility.Random(0, 100) < 10 + diff; // (10 + diff)% chance to flee
                }
                else
                {
                    flee = Utility.Random(0, 100) < 10; // 10% chance to flee
                }

                if (flee)
                {
                    if (treeState.Creature.Debug)
                        treeState.Creature.DebugSay("I am going to flee from {0}", treeState.Creature.Combatant.Name);

                    return NodeState.SUCCESS;
                }
            }

        if (treeState.CombatStartTime > 0 && Core.TickCount - treeState.CombatStartTime >
            (int)TimeSpan.FromSeconds(15).TotalMilliseconds)
        {
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }
}