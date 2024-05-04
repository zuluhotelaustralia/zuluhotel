using System;

namespace Server.Mobiles;

public class DoBeginRelease<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        treeState.Creature.DebugSay("I have been released");

        treeState.Creature.PlaySound(treeState.Creature.GetAngerSound());

        treeState.Creature.SetControlMaster(null);
        treeState.Creature.SummonMaster = null;

        var se = treeState.Creature.Spawner;
        if (se != null && se.HomeLocation != Point3D.Zero)
        {
            treeState.Creature.Home = se.HomeLocation;
            treeState.Creature.RangeHome = se.HomeRange;
        }

        if (treeState.Creature.DeleteOnRelease)
            treeState.Creature.Delete();
        else
            treeState.Creature.Say($"{treeState.Creature.Name} can roam free again!");

        treeState.Creature.BeginDeleteTimer();
        treeState.Creature.DropBackpack();

        return NodeState.SUCCESS;
    }
}