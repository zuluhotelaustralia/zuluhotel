namespace Server.Items
{
    public class SulfurousAsh : BaseReagent
    {
        [Constructible]
        public SulfurousAsh() : this(1)
        {
        }


        [Constructible]
        public SulfurousAsh(int amount) : base(0xF8C, amount)
        {
        }

        [Constructible]
        public SulfurousAsh(Serial serial) : base(serial)
        {
        }


        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
        }
    }
}