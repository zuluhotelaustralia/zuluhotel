using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class SummonMammalsScroll : SpellScroll
    {
        public override int LabelNumber { get { return 1031604; } }


        [Constructable]
        public SummonMammalsScroll() : this(1)
        {
        }

        [Constructable]
        public SummonMammalsScroll(int amount) : base(603, 0x2260, amount)
        {
        }

        public SummonMammalsScroll(Serial serial) : base(serial)
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
