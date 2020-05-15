using System;

namespace Server.Items
{
    public class VolcanicAsh : BaseReagent, ICommodity
    {
        [Constructable]
        public VolcanicAsh()
            : this(1)
        {
        }

        [Constructable]
        public VolcanicAsh(int amount)
            : base(0xF8F, amount)
        {
        }

        public VolcanicAsh(Serial serial)
            : base(serial)
        {
        }

        public override double DefaultWeight
        {
            get { return 0.1; }
        }
        int ICommodity.DescriptionNumber
        {
            get
            {
                return this.LabelNumber;
            }
        }
        bool ICommodity.IsDeedable
        {
            get
            {
                return true;
            }
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
