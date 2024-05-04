using System;

namespace Server.Mobiles;

public class DoBardPacified<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        if (DateTime.Now < treeState.Creature.BardEndTime)
        {
            treeState.Creature.DebugSay("I am pacified, I wait");
            treeState.Creature.Combatant = null;
            treeState.Creature.Warmode = false;
            
            return NodeState.RUNNING;
        }
        
        treeState.Creature.DebugSay("I'm not pacified any longer");
        treeState.Creature.BardPacified = false;
        
        return NodeState.FAILURE;
    }
}