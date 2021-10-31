using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [Flipable]
    public partial class CelestialRobe : BaseOuterTorso, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override string DefaultName => "Robe of Celestial Mastery";

        [Constructible]
        public CelestialRobe() : base(0x1F03, 1171)
        {
            Identified = false;
            MagicEfficiencyPenalty = -25.0;
            FirstSkillBonusName = SkillName.Magery;
            FirstSkillBonusValue = 10.0;
            MagicImmunity = 3;
            Weight = 3.0;
        }
    }
}