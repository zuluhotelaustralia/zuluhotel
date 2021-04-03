namespace Server.Items
{
    public class Shackles : Item
    {
        [Constructible]
        public Shackles() : base(0x1640)
        {
            Weight = 1.0;
        }

        public Shackles(Serial serial) : base(serial)
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