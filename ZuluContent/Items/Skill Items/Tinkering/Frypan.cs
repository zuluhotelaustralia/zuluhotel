namespace Server.Items
{
    public class Frypan : Item
    {
        [Constructible]
        public Frypan() : base(0x97F)
        {
            Weight = 1.0;
        }

        public Frypan(Serial serial) : base(serial)
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