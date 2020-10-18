namespace Server.Items
{
    public class BatWing : BaseReagent
    {
        [Constructible]
        public BatWing() : this(1)
        {
        }


        [Constructible]
        public BatWing(int amount) : base(0xF78, amount)
        {
        }

        [Constructible]
        public BatWing(Serial serial) : base(serial)
        {
        }

        public override double DefaultWeight
        {
            get { return 0.1; }
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