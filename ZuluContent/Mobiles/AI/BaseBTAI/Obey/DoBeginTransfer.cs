using Server.Items;
using Server.Network;

namespace Server.Mobiles;

public class DoBeginTransfer<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        var from = treeState.Creature.ControlMaster;
        var to = treeState.Creature.ControlTarget;

        if (from != to && from != null && !from.Deleted && to != null && !to.Deleted && to.Player)
        {
            treeState.Creature.DebugSay("Begin transfer with {0}", to.Name);

            var youngFrom = from is PlayerMobile ? ((PlayerMobile)from).Young : false;
            var youngTo = to is PlayerMobile ? ((PlayerMobile)to).Young : false;

            if (youngFrom && !youngTo)
            {
                from.SendLocalizedMessage(502051); // As a young player, you may not transfer pets to older players.
            }
            else if (!youngFrom && youngTo)
            {
                from.SendLocalizedMessage(
                    502052); // As an older player, you may not transfer pets to young players.
            }
            else if (!treeState.Creature.CanBeControlledBy(to))
            {
                var args = string.Format("{0}\t{1}\t ", to.Name, from.Name);

                from.SendLocalizedMessage(1043248,
                    args); // The pet refuses to be transferred because it will not obey ~1_NAME~.~3_BLANK~
                to.SendLocalizedMessage(1043249,
                    args); // The pet will not accept you as a master because it does not trust you.~3_BLANK~
            }
            else if (!treeState.Creature.CanBeControlledBy(from))
            {
                var args = string.Format("{0}\t{1}\t ", to.Name, from.Name);

                from.SendLocalizedMessage(1043250,
                    args); // The pet refuses to be transferred because it will not obey you sufficiently.~3_BLANK~
                to.SendLocalizedMessage(1043251,
                    args); // The pet will not accept you as a master because it does not trust ~2_NAME~.~3_BLANK~
            }
            else if (TransferItem.IsInCombat(treeState.Creature))
            {
                from.SendMessage("You may not transfer a pet that has recently been in combat.");
                to.SendMessage("The pet may not be transfered to you because it has recently been in combat.");
            }
            else
            {
                NetState fromState = from.NetState, toState = to.NetState;

                if (fromState != null && toState != null)
                {
                    if (from.HasTrade)
                    {
                        from.SendLocalizedMessage(1010507); // You cannot transfer a pet with a trade pending
                    }
                    else if (to.HasTrade)
                    {
                        to.SendLocalizedMessage(1010507); // You cannot transfer a pet with a trade pending
                    }
                    else
                    {
                        Container c = fromState.AddTrade(toState);
                        c.DropItem(new TransferItem(treeState.Creature));
                    }
                }
            }
        }

        treeState.Creature.ControlTarget = null;
        treeState.Creature.ControlOrder = OrderType.Stay;

        return NodeState.SUCCESS;
    }

    private class TransferItem : Item
    {
        public static bool IsInCombat(BaseCreature creature)
        {
            return creature != null && (creature.Aggressors.Count > 0 || creature.Aggressed.Count > 0);
        }

        private readonly BaseCreature m_Creature;

        public TransferItem(BaseCreature creature)
            : base(ShrinkTable.Lookup(creature))
        {
            m_Creature = creature;

            Movable = false;

            Name = creature.Name;

            //(As Per OSI)No name.  Normally, set by the ItemID of the Shrink Item unless we either explicitly set it with an Attribute, or, no lookup found

            Hue = creature.Hue & 0x0FFF;
        }

        public TransferItem(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();

            Delete();
        }

        public override bool AllowSecureTrade(Mobile from, Mobile to, Mobile newOwner, bool accepted)
        {
            if (!base.AllowSecureTrade(from, to, newOwner, accepted))
                return false;

            if (Deleted || m_Creature == null || m_Creature.Deleted || m_Creature.ControlMaster != from ||
                !from.CheckAlive() || !to.CheckAlive())
                return false;

            if (from.Map != m_Creature.Map || !from.InRange(m_Creature, 14))
                return false;

            var youngFrom = from is PlayerMobile ? ((PlayerMobile)from).Young : false;
            var youngTo = to is PlayerMobile ? ((PlayerMobile)to).Young : false;

            if (accepted && youngFrom && !youngTo)
                from.SendLocalizedMessage(502051); // As a young player, you may not transfer pets to older players.
            else if (accepted && !youngFrom && youngTo)
                from.SendLocalizedMessage(
                    502052); // As an older player, you may not transfer pets to young players.

            return true;
        }

        public override void OnSecureTrade(Mobile from, Mobile to, Mobile newOwner, bool accepted)
        {
            if (Deleted)
                return;

            Delete();

            if (m_Creature == null || m_Creature.Deleted || m_Creature.ControlMaster != from ||
                !from.CheckAlive() || !to.CheckAlive())
                return;

            if (from.Map != m_Creature.Map || !from.InRange(m_Creature, 14))
                return;

            if (accepted)
                if (m_Creature.SetControlMaster(to))
                {
                    if (m_Creature.Summoned)
                        m_Creature.SummonMaster = to;

                    m_Creature.ControlTarget = to;
                    m_Creature.ControlOrder = OrderType.Follow;

                    m_Creature.PlaySound(m_Creature.GetIdleSound());

                    var args = string.Format("{0}\t{1}\t{2}", from.Name, m_Creature.Name, to.Name);

                    from.SendLocalizedMessage(1043253, args); // You have transferred your pet to ~3_GETTER~.
                    to.SendLocalizedMessage(1043252,
                        args); // ~1_NAME~ has transferred the allegiance of ~2_PET_NAME~ to you.
                }
        }
    }
}