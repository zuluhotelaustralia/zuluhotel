using System;
using Server;

namespace Server.Items
{
    public class FertileDirt : BaseReagent
    {

        [Constructible]
public FertileDirt() : this(1)
        {
        }


        [Constructible]
public FertileDirt(int amount) : base(0xF81)
        {
            Stackable = true;
            Weight = 0.1;
            Amount = amount;
        }

        [Constructible]
public FertileDirt(Serial serial) : base(serial)
        {
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
