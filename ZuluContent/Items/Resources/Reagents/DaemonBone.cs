namespace Server.Items
{
    // TODO: Commodity?
    public class DaemonBone : BaseReagent
    {
        [Constructible]
        public DaemonBone() : this(1)
        {
        }


        [Constructible]
        public DaemonBone(int amount) : base(0xF80, amount)
        {
        }

        [Constructible]
        public DaemonBone(Serial serial) : base(serial)
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