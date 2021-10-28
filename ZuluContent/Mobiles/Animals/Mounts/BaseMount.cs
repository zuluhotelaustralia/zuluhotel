using System;
using Scripts.Zulu.Engines.Classes;
using Server.Network;
using ZuluContent.Configuration.Types.Creatures;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Mobiles
{
    public class BaseMount : BaseCreatureTemplate, IMount
    {
        private Mobile m_Rider;

        public virtual TimeSpan MountAbilityDelay => TimeSpan.Zero;

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime NextMountAbility { get; set; }

        protected Item InternalItem { get; private set; }

        public virtual bool AllowMaleRider => true;

        public virtual bool AllowFemaleRider => true;

        [Constructible]
        public BaseMount(string templateName) : base(templateName)
        {
            var creatureProps = ZhConfig.Creatures.Entries[templateName];

            var itemID = creatureProps.ItemID;
            if (itemID != null)
            {
                InternalItem = new MountItem(this, (int) itemID);
            }
            
            Hue = creatureProps.Hue;
        }

        [Hue]
        [CommandProperty(AccessLevel.GameMaster)]
        public override int Hue
        {
            get => base.Hue;
            set
            {
                base.Hue = value;

                if (InternalItem != null)
                    InternalItem.Hue = value;
            }
        }

        public override bool OnBeforeDeath()
        {
            var resurrect = false;
            if (Rider is PlayerMobile playerMobile)
                playerMobile.FireHook(h => h.OnDeath(playerMobile, ref resurrect));

            if (!resurrect)
                Rider = null;

            return base.OnBeforeDeath();
        }

        public override void OnAfterDelete()
        {
            if (InternalItem != null)
                InternalItem.Delete();

            InternalItem = null;

            base.OnAfterDelete();
        }

        public override void OnDelete()
        {
            Rider = null;

            base.OnDelete();
        }

        public BaseMount(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(1); // version

            writer.Write(NextMountAbility);

            writer.Write(m_Rider);
            writer.Write(InternalItem);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();

            switch (version)
            {
                case 1:
                {
                    NextMountAbility = reader.ReadDateTime();
                    goto case 0;
                }
                case 0:
                {
                    m_Rider = reader.ReadEntity<Mobile>();
                    InternalItem = reader.ReadEntity<Item>();

                    if (InternalItem == null)
                        Delete();

                    break;
                }
            }
        }

        public virtual void OnDisallowedRider(Mobile m)
        {
            m.SendMessage("You may not ride this creature.");
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!CheckMountAllowed(from))
                return;

            if (from.Mounted)
            {
                from.SendLocalizedMessage(1005583); // Please dismount first.
                return;
            }

            if (from.Female ? !AllowFemaleRider : !AllowMaleRider)
            {
                OnDisallowedRider(from);
                return;
            }

            if (from.HasTrade)
            {
                from.SendLocalizedMessage(1042317, "", 0x41); // You may not ride at this time
                return;
            }

            if (from.InRange(this, 1))
            {
                var canAccess = from.AccessLevel >= AccessLevel.GameMaster
                                || Controlled && ControlMaster == from
                                || Summoned && SummonMaster == from;

                if (canAccess)
                {
                    if (Poisoned)
                        PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1049692,
                            from.NetState); // This mount is too ill to ride.
                    else
                        Rider = from;
                }
                else if (!Controlled && !Summoned)
                {
                    // That mount does not look broken! You would have to tame it to ride it.
                    PrivateOverheadMessage(MessageType.Regular, 0x3B2, 501263, from.NetState);
                }
                else
                {
                    // This isn't your mount; it refuses to let you ride.
                    PrivateOverheadMessage(MessageType.Regular, 0x3B2, 501264, from.NetState);
                }
            }
            else
            {
                from.SendLocalizedMessage(500206); // That is too far away to ride.
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ItemID
        {
            get
            {
                if (InternalItem != null)
                    return InternalItem.ItemID;
                return 0;
            }
            set
            {
                if (InternalItem != null)
                    InternalItem.ItemID = value;
            }
        }

        public static void Dismount(Mobile m)
        {
            var mount = m.Mount;

            if (mount != null)
                mount.Rider = null;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Rider
        {
            get => m_Rider;
            set
            {
                if (m_Rider != value)
                {
                    if (value == null)
                    {
                        var loc = m_Rider.Location;
                        var map = m_Rider.Map;

                        if (map == null || map == Map.Internal)
                        {
                            loc = m_Rider.LogoutLocation;
                            map = m_Rider.LogoutMap;
                        }

                        Direction = m_Rider.Direction;
                        Location = loc;
                        Map = map;

                        if (InternalItem != null)
                            InternalItem.Internalize();
                    }
                    else
                    {
                        if (m_Rider != null) Dismount(m_Rider);

                        Dismount(value);

                        if (InternalItem != null)
                            value.AddItem(InternalItem);

                        value.Direction = Direction;

                        Internalize();
                    }

                    m_Rider = value;
                }
            }
        }

        // 1040024 You are still too dazed from being knocked off your mount to ride!
        // 1062910 You cannot mount while recovering from a bola throw.
        // 1070859 You cannot mount while recovering from a dismount special maneuver.

        public static bool CheckMountAllowed(Mobile mob)
        {
            var result = true;
            if (mob is PlayerMobile playerMobile && playerMobile.MountBlockReason != BlockMountType.None)
            {
                playerMobile.SendLocalizedMessage((int)playerMobile.MountBlockReason);
                result = false;
            }

            return result;
        }

        public virtual void OnRiderDamaged(int amount, Mobile from, bool willKill)
        {
            if (m_Rider == null)
                return;

            var attacker = from;
            if (attacker == null)
                attacker = m_Rider.FindMostRecentDamager(true);

            if (!(attacker == this || attacker == m_Rider || willKill || DateTime.Now < NextMountAbility))
                if (DoMountAbility(amount, @from))
                    NextMountAbility = DateTime.Now + MountAbilityDelay;
        }

        public virtual bool DoMountAbility(int damage, Mobile attacker)
        {
            return false;
        }
    }

    public class MountItem : Item, IMountItem
    {
        private BaseMount m_Mount;

        public override double DefaultWeight => 0;

        public MountItem(BaseMount mount, int itemID) : base(itemID)
        {
            Layer = Layer.Mount;
            Movable = false;

            m_Mount = mount;
        }

        public MountItem(Serial serial) : base(serial)
        {
        }

        public override void OnAfterDelete()
        {
            if (m_Mount != null)
                m_Mount.Delete();

            m_Mount = null;

            base.OnAfterDelete();
        }

        public override DeathMoveResult OnParentDeath(Mobile parent)
        {
            if (m_Mount != null)
            {
                var resurrect = false;
                if (parent is PlayerMobile playerMobile)
                    playerMobile.FireHook(h => h.OnDeath(playerMobile, ref resurrect));

                if (!resurrect)
                    m_Mount.Rider = null;
            }

            return DeathMoveResult.RemainEquipped;
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version

            writer.Write(m_Mount);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();

            switch (version)
            {
                case 0:
                {
                    m_Mount = reader.ReadEntity<BaseMount>();

                    if (m_Mount == null)
                        Delete();

                    break;
                }
            }
        }

        public IMount Mount => m_Mount;
    }
}