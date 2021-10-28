namespace Server.Mobiles
{
    public class ForestOstard : BaseMount
    {
        [Constructible]
        public ForestOstard(string name) : base("ForestOstard")
        {
        }

        [Constructible]
        public ForestOstard(Serial serial) : base(serial)
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