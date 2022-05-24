using System;
using Server.Network;

namespace Server.Items
{
    public enum GasTrapType
    {
        NorthWall,
        WestWall,
        Floor
    }

    public class GasTrap : BaseTrap
    {
        private Poison m_Poison;

        [CommandProperty(AccessLevel.GameMaster)]
        public Poison Poison
        {
            get { return m_Poison; }
            set { m_Poison = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public GasTrapType Type
        {
            get
            {
                switch (ItemID)
                {
                    case 0x113C: return GasTrapType.NorthWall;
                    case 0x1147: return GasTrapType.WestWall;
                    case 0x11A8: return GasTrapType.Floor;
                }

                return GasTrapType.WestWall;
            }
            set { ItemID = GetBaseID(value); }
        }

        public static int GetBaseID(GasTrapType type)
        {
            switch (type)
            {
                case GasTrapType.NorthWall: return 0x113C;
                case GasTrapType.WestWall: return 0x1147;
                case GasTrapType.Floor: return 0x11A8;
            }

            return 0;
        }


        [Constructible]
        public GasTrap() : this(GasTrapType.Floor)
        {
        }


        [Constructible]
        public GasTrap(GasTrapType type) : this(type, Poison.Lesser)
        {
        }


        [Constructible]
        public GasTrap(Poison poison) : this(GasTrapType.Floor, Poison.Lesser)
        {
        }


        [Constructible]
        public GasTrap(GasTrapType type, Poison poison) : base(GetBaseID(type))
        {
            m_Poison = poison;
        }

        public override bool PassivelyTriggered
        {
            get { return false; }
        }

        public override TimeSpan PassiveTriggerDelay
        {
            get { return TimeSpan.Zero; }
        }

        public override int PassiveTriggerRange
        {
            get { return 0; }
        }

        public override TimeSpan ResetDelay
        {
            get { return TimeSpan.FromSeconds(0.0); }
        }

        public override void OnTrigger(Mobile from)
        {
            if (m_Poison == null || !from.Player || !from.Alive || from.AccessLevel > AccessLevel.Player)
                return;

            Effects.SendLocationEffect(Location, Map, GetBaseID(Type) - 2, 16, 3, GetEffectHue(), 0);
            Effects.PlaySound(Location, Map, 0x231);

            from.ApplyPoison(from, m_Poison);

            from.LocalOverheadMessage(MessageType.Regular, 0x22, 500855); // You are enveloped by a noxious gas cloud!
        }

        [Constructible]
        public GasTrap(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version

            writer.Write(m_Poison);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();

            switch (version)
            {
                case 0:
                {
                    m_Poison = reader.ReadPoison();
                    break;
                }
            }
        }
    }
}