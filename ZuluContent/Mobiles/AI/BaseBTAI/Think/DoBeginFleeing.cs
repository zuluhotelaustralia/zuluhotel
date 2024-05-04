using System;

namespace Server.Mobiles;

public class DoBeginFleeing<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        treeState.Creature.FocusMob = treeState.Creature.Combatant;
        
        var nextStopFlee = Core.TickCount + (int)TimeSpan.FromSeconds(10).TotalMilliseconds;

        treeState.IsFleeing = true;
        treeState.NextStopFlee = nextStopFlee;
        
        treeState.Creature.DebugSay("Beginning to flee");
        
        return NodeState.SUCCESS;
    }
}