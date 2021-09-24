#pragma warning disable

namespace Server.Items
{
    public partial class BaseCrop
    {
        private const int _version = 0;

        public int HarvestAmount
        {
            get => _harvestAmount;
            set
            {
                if (value != _harvestAmount)
                {
                    _harvestAmount = value;
                    ((ISerializable)this).MarkDirty();
                }
            }
        }

        public BaseCrop(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(_version);

            writer.Write(HarvestAmount);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();

            HarvestAmount = reader.ReadInt();
        }
    }
}
