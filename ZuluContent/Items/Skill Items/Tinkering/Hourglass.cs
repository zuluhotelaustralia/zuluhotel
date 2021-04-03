namespace Server.Items
{
    public class Hourglass : Item
    {
        [Constructible]
        public Hourglass() : base(0x1810)
        {
            Weight = 1.0;
        }

        [Constructible]
        public Hourglass(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}