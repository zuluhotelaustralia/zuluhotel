namespace Server.Items
{
    [Serializable(0, false)]
    public partial class SkillTrainingDeed : Item
    {
        [SerializableField(0)]
        private int _credits;
        
        [SerializableField(1)]
        private Mobile _player;

        public override string DefaultName => "Skill Training Deed";

        [Constructible]
        public SkillTrainingDeed(Mobile player, int credits) : base(0x14EF)
        {
            Hue = 0x2F;
            _player = player;
            _credits = credits;
            Weight = 1.0;
        }

        public override bool DisplayLootType
        {
            get => false;
        }

        public override void OnSingleClick(Mobile from)
        {
            LabelTo(from, Name);
            LabelTo(from, $"({Credits} credits remaining)");
            LabelTo(from, $"[bound to {Player.Name}]");
        }
    }
}