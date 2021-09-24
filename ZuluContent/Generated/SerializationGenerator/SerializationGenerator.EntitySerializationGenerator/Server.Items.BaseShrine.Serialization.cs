#pragma warning disable

namespace Server.Items
{
    public partial class BaseShrine
    {
        private const int _version = 0;

        public int Piece
        {
            get => _piece;
            set
            {
                if (value != _piece)
                {
                    _piece = value;
                    ((ISerializable)this).MarkDirty();
                }
            }
        }

        public BaseShrine(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(_version);

            writer.Write(Piece);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();

            Piece = reader.ReadInt();
        }
    }
}
