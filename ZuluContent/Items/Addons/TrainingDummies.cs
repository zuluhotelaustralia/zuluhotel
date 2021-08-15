using System;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;

namespace Server.Items
{
    [Flipable(0x1070, 0x1074)]
    public class TrainingDummy : AddonComponent
    {
        private Timer m_Timer;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Swinging => m_Timer != null;


        [Constructible]
        public TrainingDummy() : this(0x1074)
        {
        }


        [Constructible]
        public TrainingDummy(int itemID) : base(itemID)
        {
        }

        public void UpdateItemID()
        {
            var baseItemID = ItemID / 2 * 2;

            ItemID = baseItemID + (Swinging ? 1 : 0);
        }

        public void BeginSwing()
        {
            m_Timer?.Stop();

            m_Timer = new InternalTimer(this);
            m_Timer.Start();
        }

        public void EndSwing()
        {
            m_Timer?.Stop();

            m_Timer = null;

            UpdateItemID();
        }

        public void OnHit()
        {
            UpdateItemID();
            Effects.PlaySound(GetWorldLocation(), Map, Utility.RandomList(0x3A4, 0x3A6, 0x3A9, 0x3AE, 0x3B4, 0x3B6));
        }

        public void Use(Mobile from, BaseWeapon weapon)
        {
            BeginSwing();

            from.Direction = from.GetDirectionTo(GetWorldLocation());
            weapon.PlaySwingAnimation(from);

            from.ShilCheckSkill(weapon.Skill, 10, 40);
            from.ShilCheckSkill(SkillName.Tactics, 10, 20);
        }

        public override void OnDoubleClick(Mobile from)
        {
            var weapon = from.Weapon as BaseWeapon;

            if (weapon is BaseRanged)
                from.SendFailureMessage(501822); // You can't practice ranged weapons on this.
            else if (weapon == null || !from.InRange(GetWorldLocation(), weapon.MaxRange))
                from.SendFailureMessage(501816); // You are too far away to do that.
            else if (Swinging)
                from.SendFailureMessage(501815); // You have to wait until it stops swinging.
            else if (from.Skills[weapon.Skill].Base >= 25.0)
                from.SendFailureMessage(501828); // Your skill cannot improve any further by simply practicing with a dummy.
            else
                Use(from, weapon);
        }

        [Constructible]
        public TrainingDummy(Serial serial) : base(serial)
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
            private readonly TrainingDummy m_Dummy;
            private bool m_Delay = true;

            public InternalTimer(TrainingDummy dummy) : base(TimeSpan.FromSeconds(0.25), TimeSpan.FromSeconds(2.75))
            {
                m_Dummy = dummy;
            }

            protected override void OnTick()
            {
                if (m_Delay)
                    m_Dummy.OnHit();
                else
                    m_Dummy.EndSwing();

                m_Delay = !m_Delay;
            }
        }
    }

    public class TrainingDummyEastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new TrainingDummyEastDeed();


        public TrainingDummyEastAddon()
        {
            AddComponent(new TrainingDummy(0x1074), 0, 0, 0);
        }

        public TrainingDummyEastAddon(Serial serial) : base(serial)
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

    public class TrainingDummyEastDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new TrainingDummyEastAddon();
        public override int LabelNumber => 1044335; // training dummy (east)


        public TrainingDummyEastDeed()
        {
        }

        public TrainingDummyEastDeed(Serial serial) : base(serial)
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

    public class TrainingDummySouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new TrainingDummySouthDeed();


        public TrainingDummySouthAddon()
        {
            AddComponent(new TrainingDummy(0x1070), 0, 0, 0);
        }

        public TrainingDummySouthAddon(Serial serial) : base(serial)
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

    public class TrainingDummySouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new TrainingDummySouthAddon();
        public override int LabelNumber => 1044336; // training dummy (south)


        public TrainingDummySouthDeed()
        {
        }

        public TrainingDummySouthDeed(Serial serial) : base(serial)
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