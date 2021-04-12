namespace Server.Items
{
    public class Kettle : BaseTinkerItem
    {
        [Constructible]
        public Kettle() : base(0x9ED)
        {
            Weight = 1.0;
        }

        public Kettle(Serial serial) : base(serial)
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