using System;

namespace Server.Items
{
    //does this need to inherit from basereagent? --sith
    public class ExecutionersCap : BaseReagent
    {
        [Constructable]
        public ExecutionersCap() : this(1)
        {
        }

        [Constructable]
        public ExecutionersCap(int amount) : base(0xF83, amount)
        {
        }

        public ExecutionersCap(Serial serial) : base(serial)
        {
        }

        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
