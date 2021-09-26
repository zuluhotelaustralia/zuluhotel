using Server.Engines.Harvest;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    public partial class XarafaxsAxe : BaseAxe, IUsesRemaining, IGMItem
    {
        public override HarvestSystem HarvestSystem => Lumberjacking.System;

        public override int DefaultStrengthReq => 20;

        public override int DefaultMinDamage => 1;

        public override int DefaultMaxDamage => 15;

        public override int DefaultSpeed => 40;

        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Bash1H;


        [Constructible]
        public XarafaxsAxe() : this(200)
        {
        }


        [Constructible]
        public XarafaxsAxe(int uses) : base(0x0F49)
        {
            Name = "Xarafax's Axe";
            Weight = 11.0;
            Hue = 1162;
            UsesRemaining = uses;
            ShowUsesRemaining = true;
            HarvestBonus = 2;
            Identified = false;
        }
    }
}