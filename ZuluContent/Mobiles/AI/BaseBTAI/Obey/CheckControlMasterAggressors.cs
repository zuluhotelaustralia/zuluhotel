namespace Server.Mobiles;

public class CheckControlMasterAggressors<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        var controlMaster = treeState.Creature.ControlMaster;
        var combatant = treeState.Creature.Combatant;
        var aggressors = controlMaster.Aggressors;

        if (aggressors.Count > 0)
        {
            for (var i = 0; i < aggressors.Count; ++i)
            {
                var info = aggressors[i];
                var attacker = info.Attacker;

                if (attacker != null && !attacker.Deleted &&
                    attacker.Alive && attacker.GetDistanceToSqrt(treeState.Creature) <=
                    treeState.Creature.RangePerception)
                    if (combatant == null || attacker.GetDistanceToSqrt(controlMaster) <
                        combatant.GetDistanceToSqrt(controlMaster))
                        combatant = attacker;
            }

            if (combatant != null)
            {
                treeState.Creature.Combatant = combatant;
                treeState.Creature.DebugSay("Crap, my master has been attacked! I will attack one of those bastards!");

                return NodeState.SUCCESS;
            }
        }

        return NodeState.FAILURE;
    }
}