using System;

namespace Server.Items
{
    public class Blackmoor : BaseReagent
    {
        [Constructable]
        public Blackmoor()
            : this(1)
        {
        }

        [Constructable]
        public Blackmoor(int amount)
            : base(0xF79, amount)
        {
        }

        public Blackmoor(Serial serial)
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

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
