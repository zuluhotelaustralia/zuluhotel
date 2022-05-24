using ModernUO.Serialization;
using Server.Engines.Harvest;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class XarafaxsAxe : BaseAxe, IUsesRemaining, IGMItem
    {
        public override HarvestSystem HarvestSystem => Lumberjacking.System;

        public override int DefaultStrengthReq => 20;

        public override int DefaultMinDamage => 1;

        public override int DefaultMaxDamage => 15;

        public override int DefaultSpeed => 40;

        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Bash1H;

        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Xarafax's Axe";

        [Constructible]
        public XarafaxsAxe() : this(1000)
        {
        }

        [Constructible]
        public XarafaxsAxe(int uses) : base(0x0F49)
        {
            Weight = 11.0;
            Hue = 1162;
            UsesRemaining = uses;
            ShowUsesRemaining = true;
            HarvestBonus = 2;
            Identified = false;
        }
    }
}