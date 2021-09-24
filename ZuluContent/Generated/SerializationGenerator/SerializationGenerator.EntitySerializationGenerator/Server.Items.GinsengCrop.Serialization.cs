#pragma warning disable

namespace Server.Items
{
    public partial class GinsengCrop
    {
        private const int _version = 0;

        public GinsengCrop(Serial serial) : base(serial)
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
