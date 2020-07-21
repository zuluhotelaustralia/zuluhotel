using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class PigIron : BaseReagent
    {


        [Constructible]
public PigIron() : this(1)
        {
        }


        [Constructible]
public PigIron(int amount) : base(0xF8A, amount)
        {
        }

        [Constructible]
public PigIron(Serial serial) : base(serial)
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
