using System;

namespace Server.Items
{
    public class Obsidian : BaseReagent
    {
        [Constructable]
        public Obsidian()
            : this(1)
        {
        }

        [Constructable]
        public Obsidian(int amount)
            : base(0xF89, amount)
        {
        }

        public Obsidian(Serial serial)
            : base(serial)
        {
        }

        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}