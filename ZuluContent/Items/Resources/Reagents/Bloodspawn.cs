using System;

namespace Server.Items
{
    public class Bloodspawn : BaseReagent
    {

        [Constructible]
public Bloodspawn()
            : this(1)
        {
        }


        [Constructible]
public Bloodspawn(int amount)
            : base(0xF7C, amount)
        {
        }

        [Constructible]
public Bloodspawn(Serial serial)
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
