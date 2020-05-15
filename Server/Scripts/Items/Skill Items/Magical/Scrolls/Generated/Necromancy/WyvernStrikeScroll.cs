using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class WyvernStrikeScroll : SpellScroll
    {
        public override int LabelNumber { get { return 1060520; } }

        [Constructable]
        public WyvernStrikeScroll() : this(1)
        {
        }

        [Constructable]
        public WyvernStrikeScroll(int amount) : base(111, 0x2260, amount)
        {
            Hue = 0x66D;
        }

        public WyvernStrikeScroll(Serial serial) : base(serial)
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
