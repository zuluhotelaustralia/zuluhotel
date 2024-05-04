using Server.Network;

namespace Server.Mobiles;

public class DoBeginFriend<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        var from = treeState.Creature.ControlMaster;
        var to = treeState.Creature.ControlTarget;

        if (from == null || to == null || from == to || from.Deleted || to.Deleted || !to.Player)
        {
            treeState.Creature.PublicOverheadMessage(MessageType.Regular, 0x3B2, 502039); // *looks confused*
        }
        else
        {
            var youngFrom = from is PlayerMobile ? ((PlayerMobile)from).Young : false;
            var youngTo = to is PlayerMobile ? ((PlayerMobile)to).Young : false;

            if (youngFrom && !youngTo)
            {
                from.SendLocalizedMessage(502040); // As a young player, you may not friend pets to older players.
            }
            else if (!youngFrom && youngTo)
            {
                from.SendLocalizedMessage(502041); // As an older player, you may not friend pets to young players.
            }
            else if (from.CanBeBeneficial(to, true))
            {
                NetState fromState = from.NetState, toState = to.NetState;

                if (fromState != null && toState != null)
                {
                    if (from.HasTrade)
                    {
                        from.SendLocalizedMessage(1070947); // You cannot friend a pet with a trade pending
                    }
                    else if (to.HasTrade)
                    {
                        to.SendLocalizedMessage(1070947); // You cannot friend a pet with a trade pending
                    }
                    else if (treeState.Creature.IsPetFriend(to))
                    {
                        from.SendLocalizedMessage(1049691); // That person is already a friend.
                    }
                    else if (!treeState.Creature.AllowNewPetFriend)
                    {
                        from.SendLocalizedMessage(
                            1005482); // Your pet does not seem to be interested in making new friends right now.
                    }
                    else
                    {
                        // ~1_NAME~ will now accept movement commands from ~2_NAME~.
                        from.SendLocalizedMessage(1049676, string.Format("{0}\t{1}", treeState.Creature.Name, to.Name));

                        /* ~1_NAME~ has granted you the ability to give orders to their pet ~2_PET_NAME~.
                         * This creature will now consider you as a friend.
                         */
                        to.SendLocalizedMessage(1043246, string.Format("{0}\t{1}", from.Name, treeState.Creature.Name));

                        treeState.Creature.AddPetFriend(to);

                        treeState.Creature.ControlTarget = to;
                        treeState.Creature.ControlOrder = OrderType.Follow;

                        return NodeState.SUCCESS;
                    }
                }
            }
        }

        treeState.Creature.ControlTarget = from;
        treeState.Creature.ControlOrder = OrderType.Follow;

        return NodeState.SUCCESS;
    }
}