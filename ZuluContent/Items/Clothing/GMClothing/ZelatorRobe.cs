using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [Flipable]
    public partial class ZelatorRobe : BaseOuterTorso, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override string DefaultName => "Zelator's Robe";

        [Constructible]
        public ZelatorRobe() : base(0x1F03, 1165)
        {
            Identified = false;
            MagicEfficiencyPenalty = -5.0;
            MagicImmunity = 1;
            Weight = 3.0;
        }
    }
}