using System;

namespace Server.Items
{
    public class VialOfBlood : BaseReagent
    {
        [Constructable]
        public VialOfBlood()
            : this(1)
        {
        }

        [Constructable]
        public VialOfBlood(int amount)
            : base(0xF7D, amount)
        {
        }

        public VialOfBlood(Serial serial)
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
