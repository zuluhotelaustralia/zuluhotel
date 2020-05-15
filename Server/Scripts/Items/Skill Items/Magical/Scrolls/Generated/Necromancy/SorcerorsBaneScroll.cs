using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class SorcerorsBaneScroll : SpellScroll
    {
        public override int LabelNumber { get { return 1060517; } }

        [Constructable]
        public SorcerorsBaneScroll() : this(1)
        {
        }

        [Constructable]
        public SorcerorsBaneScroll(int amount) : base(108, 0x2260, amount)
        {
            Hue = 0x66D;
        }

        public SorcerorsBaneScroll(Serial serial) : base(serial)
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
