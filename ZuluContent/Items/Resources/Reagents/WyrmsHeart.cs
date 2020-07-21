using System;

namespace Server.Items
{
    public class WyrmsHeart : BaseReagent
    {

        [Constructible]
public WyrmsHeart()
            : this(1)
        {
        }


        [Constructible]
public WyrmsHeart(int amount)
            : base(0xF91, amount)
        {
        }

        [Constructible]
public WyrmsHeart(Serial serial)
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

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
