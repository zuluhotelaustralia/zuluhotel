namespace Server.Items
{
    public class SerpentScale : BaseReagent
    {
        public override string DefaultName { get; } = "Serpent's Scale";

        [Constructible]
        public SerpentScale() : this(1)
        {
        }


        [Constructible]
        public SerpentScale(int amount) : base(0x0F8E, amount)
        {
        }

        [Constructible]
        public SerpentScale(Serial serial) : base(serial)
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