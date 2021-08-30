#pragma warning disable

namespace Server.Items
{
    public partial class FlourMillSouthAddon
    {
        private const int _version = 0;

        public FlourMillSouthAddon(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(_version);

            writer.Write(CurFlour);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();

            CurFlour = reader.ReadInt();

            Timer.DelayCall(AfterDeserialization);
        }
    }
}
