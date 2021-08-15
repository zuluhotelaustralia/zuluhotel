using System;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;

namespace Server.Items
{
    [Flipable(0x1EC0, 0x1EC3)]
    public class PickpocketDip : AddonComponent
    {
        private Timer m_Timer;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Swinging => m_Timer != null;

        public PickpocketDip(int itemID) : base(itemID)
        {
        }

        public void UpdateItemID()
        {
            var baseItemID = 0x1EC0 + (ItemID - 0x1EC0) / 3 * 3;

            ItemID = baseItemID + (Swinging ? 1 : 0);
        }

        public void BeginSwing()
        {
            m_Timer?.Stop();

            m_Timer = new InternalTimer(this);
            m_Timer.Start();

            UpdateItemID();
        }

        public void EndSwing()
        {
            m_Timer?.Stop();

            m_Timer = null;

            UpdateItemID();
        }

        public void Use(Mobile from)
        {
            from.Direction = from.GetDirectionTo(GetWorldLocation());

            Effects.PlaySound(GetWorldLocation(), Map, 0x4F);

            if (from.ShilCheckSkill(SkillName.Stealing, 10, 200))
            {
                from.SendSuccessMessage(501834); // You successfully avoid disturbing the dip while searching it.
            }
            else
            {
                Effects.PlaySound(GetWorldLocation(), Map, 0x390);

                BeginSwing();
                ProcessDelta();
                from.SendFailureMessage(501831); // You carelessly bump the dip and start it swinging.
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.InRange(GetWorldLocation(), 1))
                from.SendFailureMessage(501816); // You are too far away to do that.
            else if (Swinging)
                from.SendFailureMessage(501815); // You have to wait until it stops swinging.
            else
                Use(from);
        }

        public PickpocketDip(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
            
            UpdateItemID();
        }

        private class InternalTimer : Timer
        {
            private readonly PickpocketDip m_Dip;

            public InternalTimer(PickpocketDip dip) : base(TimeSpan.FromSeconds(3.0))
            {
                m_Dip = dip;
            }

            protected override void OnTick()
            {
                m_Dip.EndSwing();
            }
        }
    }

    public class PickpocketDipEastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new PickpocketDipEastDeed();


        [Constructible]
        public PickpocketDipEastAddon()
        {
            AddComponent(new PickpocketDip(0x1EC3), 0, 0, 0);
        }

        [Constructible]
        public PickpocketDipEastAddon(Serial serial) : base(serial)
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
        }
    }

    public class PickpocketDipEastDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new PickpocketDipEastAddon();
        public override int LabelNumber => 1044337; // pickpocket dip (east)


        public PickpocketDipEastDeed()
        {
        }

        public PickpocketDipEastDeed(Serial serial) : base(serial)
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
        }
    }

    public class PickpocketDipSouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new PickpocketDipSouthDeed();


        public PickpocketDipSouthAddon()
        {
            AddComponent(new PickpocketDip(0x1EC0), 0, 0, 0);
        }

        public PickpocketDipSouthAddon(Serial serial) : base(serial)
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
        }
    }

    public class PickpocketDipSouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new PickpocketDipSouthAddon();
        public override int LabelNumber => 1044338; // pickpocket dip (south)


        public PickpocketDipSouthDeed()
        {
        }

        public PickpocketDipSouthDeed(Serial serial) : base(serial)
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
        }
    }
}