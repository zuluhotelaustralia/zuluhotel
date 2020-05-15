using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
    public class PoisonedBolt : Bolt
    {
        private Poison m_Poison;
        [CommandPropertyAttribute(AccessLevel.GameMaster)]
        public Poison Poison
        {
            get { return m_Poison; }
            set { m_Poison = value; }
        }

        [Constructable]
        public PoisonedBolt() : this(1)
        {
        }

        [Constructable]
        public PoisonedBolt(int amount) : this(amount, Poison.Lesser) { }

        [Constructable]
        public PoisonedBolt(int amount, Poison psn) : base(0x1BFB)
        {
            Stackable = true;
            Amount = amount;

            m_Poison = psn;

            Hue = 1372;

            Name = "poisoned bolt";

        }

        public override void OnAfterDuped(Item newItem)
        {
            if (!(newItem is PoisonedBolt))
                return;

            PoisonedBolt o = newItem as PoisonedBolt;
            m_Poison = o.m_Poison;
        }

        public override void OnHit(Mobile from, Mobile targ)
        {
            targ.ApplyPoison(from, m_Poison);
        }

        public override bool StackWith(Mobile from, Item dropped, bool playSound)
        {
            if (dropped is PoisonedBolt)
            {
                if (((PoisonedBolt)dropped).Poison == m_Poison)
                {
                    return base.StackWith(from, dropped, playSound);
                }
            }

            return false;
        }

        public PoisonedBolt(Serial serial) : base(serial)
        {

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            Poison.Serialize(m_Poison, writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_Poison = Poison.Deserialize(reader);
        }
    }

    public class PoisonedArrow : Arrow
    {
        private Poison m_Poison;
        [CommandPropertyAttribute(AccessLevel.GameMaster)]
        public Poison Poison
        {
            get { return m_Poison; }
            set { m_Poison = value; }
        }

        [Constructable]
        public PoisonedArrow() : this(1)
        {
        }

        [Constructable]
        public PoisonedArrow(int amount) : this(amount, Poison.Lesser) { }

        [Constructable]
        public PoisonedArrow(int amount, Poison psn) : base(0xF3F)
        {
            Stackable = true;
            Amount = amount;
            Hue = 1372;

            m_Poison = psn;

            Name = "poisoned arrow";
        }

        public override void OnAfterDuped(Item newItem)
        {
            if (!(newItem is PoisonedArrow))
                return;

            PoisonedArrow o = newItem as PoisonedArrow;
            m_Poison = o.m_Poison;
        }

        public override void OnHit(Mobile from, Mobile targ)
        {
            targ.ApplyPoison(from, m_Poison);
        }

        public override bool StackWith(Mobile from, Item dropped, bool playSound)
        {
            if (dropped is PoisonedArrow)
            {
                if (((PoisonedArrow)dropped).Poison == m_Poison)
                {
                    return base.StackWith(from, dropped, playSound);
                }
            }

            return false;
        }

        public PoisonedArrow(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            Poison.Serialize(m_Poison, writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_Poison = Poison.Deserialize(reader);
        }
    }

    public class FireArrow : Item, ICommodity
    {
        int ICommodity.DescriptionNumber { get { return LabelNumber; } }
        bool ICommodity.IsDeedable { get { return true; } }

        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public FireArrow() : this(1)
        {
        }

        [Constructable]
        public FireArrow(int amount) : base(0xF3F)
        {
            Stackable = true;
            Amount = amount;
            Hue = 2747;

            if (amount > 1)
            {
                Name = amount + " fire arrows";
            }
            else
            {
                Name = "fire arrow";
            }
        }

        public FireArrow(Serial serial) : base(serial)
        {
        }



        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
