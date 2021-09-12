#pragma warning disable

namespace Server.Items
{
    public partial class ShadowElementalPentagram4
    {
        private const int _version = 0;

        public ShadowElementalPentagram4(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(_version);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
        }
    }
}
