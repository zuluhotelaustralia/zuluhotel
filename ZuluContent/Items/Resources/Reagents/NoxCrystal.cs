namespace Server.Items
{
    public class NoxCrystal : BaseReagent
    {
        [Constructible]
        public NoxCrystal() : this(1)
        {
        }


        [Constructible]
        public NoxCrystal(int amount) : base(0xF8E, amount)
        {
        }

        [Constructible]
        public NoxCrystal(Serial serial) : base(serial)
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