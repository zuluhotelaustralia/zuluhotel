using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class FireSpiritScroll : SpellScroll
    {
        public override int LabelNumber { get { return 1031614; } }


        [Constructable]
        public FireSpiritScroll() : this(1)
        {
        }

        [Constructable]
        public FireSpiritScroll(int amount) : base(613, 0x2260, amount)
        {
        }

        public FireSpiritScroll(Serial serial) : base(serial)
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
