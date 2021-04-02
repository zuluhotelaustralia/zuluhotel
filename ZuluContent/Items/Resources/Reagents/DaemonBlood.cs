namespace Server.Items
{
    public class DaemonBlood : BaseReagent
    {
        [Constructible]
        public DaemonBlood() : this(1)
        {
        }


        [Constructible]
        public DaemonBlood(int amount) : base(0xF7D, amount)
        {
        }

        [Constructible]
        public DaemonBlood(Serial serial) : base(serial)
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