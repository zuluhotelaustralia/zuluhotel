using System;
using Server.Spells;

namespace Server.Items
{
    public enum StoneFaceTrapType
    {
        NorthWestWall,
        NorthWall,
        WestWall
    }

    public class StoneFaceTrap : BaseTrap
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public StoneFaceTrapType Type
        {
            get
            {
                switch (ItemID)
                {
                    case 0x10F5:
                    case 0x10F6:
                    case 0x10F7: return StoneFaceTrapType.NorthWestWall;
                    case 0x10FC:
                    case 0x10FD:
                    case 0x10FE: return StoneFaceTrapType.NorthWall;
                    case 0x110F:
                    case 0x1110:
                    case 0x1111: return StoneFaceTrapType.WestWall;
                }

                return StoneFaceTrapType.NorthWestWall;
            }
            set
            {
                var breathing = Breathing;

                ItemID = breathing ? GetFireID(value) : GetBaseID(value);
            }
        }

        public bool Breathing
        {
            get { return ItemID == GetFireID(Type); }
            set
            {
                if (value)
                    ItemID = GetFireID(Type);
                else
                    ItemID = GetBaseID(Type);
            }
        }

        public static int GetBaseID(StoneFaceTrapType type)
        {
            switch (type)
            {
                case StoneFaceTrapType.NorthWestWall: return 0x10F5;
                case StoneFaceTrapType.NorthWall: return 0x10FC;
                case StoneFaceTrapType.WestWall: return 0x110F;
            }

            return 0;
        }

        public static int GetFireID(StoneFaceTrapType type)
        {
            switch (type)
            {
                case StoneFaceTrapType.NorthWestWall: return 0x10F7;
                case StoneFaceTrapType.NorthWall: return 0x10FE;
                case StoneFaceTrapType.WestWall: return 0x1111;
            }

            return 0;
        }


        [Constructible]
        public StoneFaceTrap() : base(0x10FC)
        {
            Light = LightType.Circle225;
        }

        public override bool PassivelyTriggered
        {
            get { return true; }
        }

        public override TimeSpan PassiveTriggerDelay
        {
            get { return TimeSpan.Zero; }
        }

        public override int PassiveTriggerRange
        {
            get { return 2; }
        }

        public override TimeSpan ResetDelay
        {
            get { return TimeSpan.Zero; }
        }

        public override void OnTrigger(Mobile from)
        {
            if (!from.Alive || from.AccessLevel > AccessLevel.Player)
                return;

            Effects.PlaySound(Location, Map, 0x359);

            Breathing = true;

            Timer.DelayCall(TimeSpan.FromSeconds(2.0), FinishBreath);
            Timer.DelayCall(TimeSpan.FromSeconds(1.0), TriggerDamage);
        }

        public virtual void FinishBreath()
        {
            Breathing = false;
        }

        public virtual void TriggerDamage()
        {
            foreach (var mob in GetMobilesInRange(1))
                if (mob.Alive && mob.AccessLevel == AccessLevel.Player)
                    SpellHelper.Damage(Utility.Dice(3, 15, 0), mob, mob, null, TimeSpan.FromTicks(1));
        }

        [Constructible]
        public StoneFaceTrap(Serial serial) : base(serial)
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

            var version = reader.ReadInt();

            Breathing = false;
        }
    }

    public class StoneFaceTrapNoDamage : StoneFaceTrap
    {
        public StoneFaceTrapNoDamage()
        {
        }

        public StoneFaceTrapNoDamage(Serial serial) : base(serial)
        {
        }

        public override void TriggerDamage()
        {
            // nothing..
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
        }
    }
}