#pragma warning disable

namespace Server.Items
{
    public partial class SkillTrainingDeed
    {
        private const int _version = 0;

        public int Credits
        {
            get => _credits;
            set
            {
                if (value != _credits)
                {
                    _credits = value;
                    ((ISerializable)this).MarkDirty();
                }
            }
        }

        public Server.Mobile Player
        {
            get => _player;
            set
            {
                if (value != _player)
                {
                    _player = value;
                    ((ISerializable)this).MarkDirty();
                }
            }
        }

        public SkillTrainingDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(_version);

            writer.Write(Credits);

            writer.Write(Player);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();

            Credits = reader.ReadInt();

            Player = reader.ReadEntity<Server.Mobile>();
        }
    }
}
