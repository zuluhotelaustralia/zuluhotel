namespace Server.Items
{
    public class ExecutionersCap : BaseReagent
    {
        [Constructible]
        public ExecutionersCap() : this(1)
        {
        }


        [Constructible]
        public ExecutionersCap(int amount) : base(0xF83, amount)
        {
        }

        [Constructible]
        public ExecutionersCap(Serial serial) : base(serial)
        {
        }

        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
        }
    }
}