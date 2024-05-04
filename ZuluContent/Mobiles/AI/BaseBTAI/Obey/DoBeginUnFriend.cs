using Server.Network;

namespace Server.Mobiles;

public class DoBeginUnFriend<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        var from = treeState.Creature.ControlMaster;
        var to = treeState.Creature.ControlTarget;

        if (from == null || to == null || from == to || from.Deleted || to.Deleted || !to.Player)
        {
            treeState.Creature.PublicOverheadMessage(MessageType.Regular, 0x3B2, 502039); // *looks confused*
        }
        else if (!treeState.Creature.IsPetFriend(to))
        {
            from.SendLocalizedMessage(1070953); // That person is not a friend.
        }
        else
        {
            // ~1_NAME~ will no longer accept movement commands from ~2_NAME~.
            from.SendLocalizedMessage(1070951, string.Format("{0}\t{1}", treeState.Creature.Name, to.Name));

            /* ~1_NAME~ has no longer granted you the ability to give orders to their pet ~2_PET_NAME~.
             * This creature will no longer consider you as a friend.
             */
            to.SendLocalizedMessage(1070952, string.Format("{0}\t{1}", from.Name, treeState.Creature.Name));

            treeState.Creature.RemovePetFriend(to);
        }

        treeState.Creature.ControlTarget = from;
        treeState.Creature.ControlOrder = OrderType.Follow;

        return NodeState.SUCCESS;
    }
}