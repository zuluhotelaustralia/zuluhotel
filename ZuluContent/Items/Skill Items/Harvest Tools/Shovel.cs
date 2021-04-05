using Server.Engines.Harvest;

namespace Server.Items
{
    public class Shovel : BaseHarvestTool
    {
        public override HarvestSystem HarvestSystem => Mining.System;
        
        [Constructible]
        public Shovel() : this(50)
        {
        }


        [Constructible]
        public Shovel(int uses) : base(uses, 0xF39)
        {
            Weight = 5.0;
        }

        [Constructible]
        public Shovel(Serial serial) : base(serial)
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