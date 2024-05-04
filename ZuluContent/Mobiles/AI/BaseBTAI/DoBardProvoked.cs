using System;

namespace Server.Mobiles;

public class DoBardProvoked<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        if (DateTime.Now >= treeState.Creature.BardEndTime && (treeState.Creature.BardMaster == null || treeState.Creature.BardMaster.Deleted ||
                                                               treeState.Creature.BardMaster.Map != treeState.Creature.Map ||
                                                               treeState.Creature.GetDistanceToSqrt(treeState.Creature.BardMaster) >
                                                               treeState.Creature.RangePerception))
        {
            treeState.Creature.DebugSay("I have lost my provoker");
            treeState.Creature.BardProvoked = false;
            treeState.Creature.BardMaster = null;
            treeState.Creature.BardTarget = null;

            treeState.Creature.Combatant = null;
            treeState.Creature.Warmode = false;
            
            return NodeState.FAILURE;
        }
        
        if (treeState.Creature.BardTarget == null || treeState.Creature.BardTarget.Deleted ||
            treeState.Creature.BardTarget.Map != treeState.Creature.Map ||
            treeState.Creature.GetDistanceToSqrt(treeState.Creature.BardTarget) > treeState.Creature.RangePerception)
        {
            treeState.Creature.DebugSay("I have lost my provoke target");
            treeState.Creature.BardProvoked = false;
            treeState.Creature.BardMaster = null;
            treeState.Creature.BardTarget = null;

            treeState.Creature.Combatant = null;
            treeState.Creature.Warmode = false;
            
            return NodeState.FAILURE;
        }
        
        treeState.Creature.Combatant = treeState.Creature.BardTarget;
        treeState.Creature.Warmode = true;
        treeState.Creature.FocusMob = null;
        treeState.Creature.CurrentSpeed = treeState.Creature.ActiveSpeed;
        
        return NodeState.RUNNING;
    }
}