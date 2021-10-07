using Server.Engines.Harvest;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    public partial class OmerosPickaxe : BaseAxe, IUsesRemaining, IGMItem
    {
        public override HarvestSystem HarvestSystem => Mining.System;

        public override int DefaultStrengthReq => 20;

        public override int DefaultMinDamage => 1;

        public override int DefaultMaxDamage => 15;

        public override int DefaultSpeed => 40;

        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Slash1H;
        
        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Omero's Pickaxe";
        
        [Constructible]
        public OmerosPickaxe() : this(1000)
        {
        }

        [Constructible]
        public OmerosPickaxe(int uses) : base(0xE86)
        {
            Weight = 11.0;
            Hue = 1301;
            UsesRemaining = uses;
            ShowUsesRemaining = true;
            HarvestBonus = 2;
            Identified = false;
        }
    }
}