using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [Flipable]
    public partial class MagisterRobe : BaseOuterTorso, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override string DefaultName => "Magister's Robe";

        [Constructible]
        public MagisterRobe() : base(0x1F03, 1174)
        {
            Identified = false;
            MagicEfficiencyPenalty = -15.0;
            MagicImmunity = 2;
            Weight = 3.0;
        }
    }
}