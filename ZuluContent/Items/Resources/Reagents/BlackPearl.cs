using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class BlackPearl : BaseReagent
    {

        [Constructible]
public BlackPearl() : this(1)
        {
        }


        [Constructible]
public BlackPearl(int amount) : base(0xF7A, amount)
        {
        }

        [Constructible]
public BlackPearl(Serial serial) : base(serial)
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
