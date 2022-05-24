using ModernUO.Serialization;
using Server.Engines.Harvest;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class AxeOfAnias : BaseAxe, IUsesRemaining, IGMItem
    {
        public override HarvestSystem HarvestSystem => Lumberjacking.System;

        public override int DefaultStrengthReq => 100;

        public override int DefaultMinDamage => 39;

        public override int DefaultMaxDamage => 55;

        public override int DefaultSpeed => 33;

        public override int InitMinHits => 200;

        public override int InitMaxHits => 200;

        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Axe of Anias";

        [Constructible]
        public AxeOfAnias() : this(1000)
        {
        }

        [Constructible]
        public AxeOfAnias(int uses) : base(0x0F45)
        {
            Weight = 11.0;
            Hue = 1158;
            UsesRemaining = uses;
            ShowUsesRemaining = true;
            Identified = false;
        }
    }
}