using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [Flipable]
    public partial class MagisterMundiRobe : BaseOuterTorso, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override string DefaultName => "Magister Mundi's Robe";

        [Constructible]
        public MagisterMundiRobe() : base(0x1F03, 1172)
        {
            Identified = false;
            MagicEfficiencyPenalty = -20.0;
            MagicImmunity = 3;
            Weight = 3.0;
        }
    }
}