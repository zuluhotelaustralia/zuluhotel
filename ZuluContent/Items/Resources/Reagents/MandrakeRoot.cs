using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class MandrakeRoot : BaseReagent
    {

        [Constructible]
public MandrakeRoot() : this(1)
        {
        }


        [Constructible]
public MandrakeRoot(int amount) : base(0xF86, amount)
        {
        }

        [Constructible]
public MandrakeRoot(Serial serial) : base(serial)
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
