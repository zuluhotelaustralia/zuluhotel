using System;

namespace Server.Items
{
    public class VialOfBlood : BaseReagent
    {

        [Constructible]
public VialOfBlood()
            : this(1)
        {
        }


        [Constructible]
public VialOfBlood(int amount)
            : base(0xF7D, amount)
        {
        }

        [Constructible]
public VialOfBlood(Serial serial)
            : base(serial)
        {
        }

        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
