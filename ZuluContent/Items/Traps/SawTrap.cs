using System;
using Server.Network;
using Server.Spells;

namespace Server.Items
{
    public enum SawTrapType
    {
        WestWall,
        NorthWall,
        WestFloor,
        NorthFloor
    }

    public class SawTrap : BaseTrap
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public SawTrapType Type
        {
            get
            {
                return ItemID switch
                {
                    0x1103 => SawTrapType.NorthWall,
                    0x1116 => SawTrapType.WestWall,
                    0x11AC => SawTrapType.NorthFloor,
                    0x11B1 => SawTrapType.WestFloor,
                    _ => SawTrapType.NorthWall
                };
            }
            set { ItemID = GetBaseId(value); }
        }

        public static int GetBaseId(SawTrapType type)
        {
            return type switch
            {
                SawTrapType.NorthWall => 0x1103,
                SawTrapType.WestWall => 0x1116,
                SawTrapType.NorthFloor => 0x11AC,
                SawTrapType.WestFloor => 0x11B1,
                _ => 0
            };
        }


        [Constructible]
        public SawTrap() : this(SawTrapType.NorthFloor)
        {
        }


        [Constructible]
        public SawTrap(SawTrapType type) : base(GetBaseId(type))
        {
        }

        public override bool PassivelyTriggered { get; } = false;

        public override TimeSpan PassiveTriggerDelay { get; } = TimeSpan.Zero;

        public override int PassiveTriggerRange { get; } = 0;

        public override TimeSpan ResetDelay { get; } = TimeSpan.FromSeconds(0.0);

        public override void OnTrigger(Mobile from)
        {
            if (!from.Alive || from.AccessLevel > AccessLevel.Player)
                return;

            Effects.SendLocationEffect(Location, Map, GetBaseId(Type) + 1, 6, 3, GetEffectHue(), 0);
            Effects.PlaySound(Location, Map, 0x21C);

            SpellHelper.Damage(Utility.RandomMinMax(5, 15), from, from, null, TimeSpan.FromTicks(1));

            from.LocalOverheadMessage(MessageType.Regular, 0x22, 500853); // You stepped onto a blade trap!
        }

        [Constructible]
        public SawTrap(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}