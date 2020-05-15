using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class KillScroll : SpellScroll
    {
        public override int LabelNumber { get { return 1060521; } }

        [Constructable]
        public KillScroll() : this(1)
        {
        }

        [Constructable]
        public KillScroll(int amount) : base(112, 0x2260, amount)
        {
            Hue = 0x66D;
        }

        public KillScroll(Serial serial) : base(serial)
        {
            Hue = 0x66D;
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
