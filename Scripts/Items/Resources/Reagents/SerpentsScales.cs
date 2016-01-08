using System;

namespace Server.Items
{
    public class SerpentsScales : BaseReagent, ICommodity
    {
        [Constructable]
        public SerpentsScales()
            : this(1)
        {
        }

        [Constructable]
        public SerpentsScales(int amount)
            : base(0xF8E, amount)
        {
        }

        public SerpentsScales(Serial serial)
            : base(serial)
        {
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