using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class Bloodmoss : BaseReagent
    {


        [Constructible]
public Bloodmoss() : this(1)
        {
        }


        [Constructible]
public Bloodmoss(int amount) : base(0xF7B, amount)
        {
        }

        [Constructible]
public Bloodmoss(Serial serial) : base(serial)
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
