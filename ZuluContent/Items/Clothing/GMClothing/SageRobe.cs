using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [Flipable]
    public partial class SageRobe : BaseOuterTorso, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override string DefaultName => "Sage's Robe";

        [Constructible]
        public SageRobe() : base(0x1F03, 1171)
        {
            Identified = false;
            MagicEfficiencyPenalty = -15.0;
            FirstSkillBonusName = SkillName.Magery;
            FirstSkillBonusValue = 5.0;
            Weight = 3.0;
        }
    }
}