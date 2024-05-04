using System;

namespace Server.Mobiles;

public class DoBeginGuarding<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        treeState.Creature.Warmode = true;
        treeState.Creature.FocusMob = null;
        treeState.Creature.Combatant = null;
        treeState.Creature.CurrentSpeed = treeState.Creature.ActiveSpeed;

        var nextStopGuard = Core.TickCount + (int)TimeSpan.FromSeconds(10).TotalMilliseconds;

        treeState.IsGuarding = true;
        treeState.NextStopGuard = nextStopGuard;

        treeState.Creature.DebugSay("Beginning to guard");
        
        return NodeState.SUCCESS;
    }
}