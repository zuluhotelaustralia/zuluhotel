#pragma warning disable

namespace Server.Items
{
    public partial class SeedDirt
    {
        private const int _version = 0;

        public Server.Items.BaseCrop Crop
        {
            get => _crop;
            set
            {
                if (value != _crop)
                {
                    _crop = value;
                    ((ISerializable)this).MarkDirty();
                }
            }
        }

        public SeedDirt(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(_version);

            writer.Write(Crop);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();

            Crop = reader.ReadEntity<Server.Items.BaseCrop>();

            Timer.DelayCall(OnAfterDeserialization);
        }
    }
}
