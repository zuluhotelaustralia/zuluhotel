using System;

namespace Server.Mobiles;

public class CheckFleeing<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        var fleeing = treeState.IsFleeing;
        var nextStopFlee = treeState.NextStopFlee;
        
       var checkHitsRecovered = treeState.CombatStartTime > 0 && Core.TickCount - treeState.CombatStartTime >
           (int)TimeSpan.FromSeconds(15).TotalMilliseconds;
       var hitsRecovered = treeState.Creature.Hits < treeState.Creature.HitsMax / 2;

        if (fleeing && Core.TickCount < nextStopFlee && (!checkHitsRecovered || hitsRecovered))
        {
            return NodeState.SUCCESS;
        }
        
        return NodeState.FAILURE;
    }
}