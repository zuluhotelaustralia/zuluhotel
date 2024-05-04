namespace Server.Mobiles;

public class CheckMapSectorInactive<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        if (treeState.Creature.PlayerRangeSensitive)
        {
            var sect = treeState.Creature.Map.GetSector(treeState.Creature.Location);

            if (!sect.Active)
            {
                return NodeState.SUCCESS;
            }
        }
        
        return NodeState.FAILURE;
    }
}