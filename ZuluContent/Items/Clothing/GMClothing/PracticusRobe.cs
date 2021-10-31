using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [Flipable]
    public partial class PracticusRobe : BaseOuterTorso, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override string DefaultName => "Practicus's Robe";

        [Constructible]
        public PracticusRobe() : base(0x1F03, 1301)
        {
            Identified = false;
            MagicEfficiencyPenalty = -10.0;
            MagicImmunity = 2;
            Weight = 3.0;
        }
    }
}