namespace Server.Items
{
    public class Cauldron : Item
    {
        [Constructible]
        public Cauldron() : base(0x975)
        {
            Weight = 1.0;
        }

        public Cauldron(Serial serial) : base(serial)
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