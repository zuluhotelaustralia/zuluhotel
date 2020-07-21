using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class Ginseng : BaseReagent
    {

        [Constructible]
public Ginseng() : this(1)
        {
        }


        [Constructible]
public Ginseng(int amount) : base(0xF85, amount)
        {
        }

        [Constructible]
public Ginseng(Serial serial) : base(serial)
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
