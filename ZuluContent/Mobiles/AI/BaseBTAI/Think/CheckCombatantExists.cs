namespace Server.Mobiles;

public class CheckCombatantExists<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        var combatant = treeState.Creature.Combatant;

        if (combatant == null || combatant.Deleted || combatant.Map != treeState.Creature.Map || !combatant.Alive)
        {
            treeState.Creature.DebugSay("My combatant is gone");
            
            return NodeState.FAILURE;
        }
        
        return NodeState.SUCCESS;
    }
}