using Server.Engines.Harvest;
using Server.Mobiles;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x13FF, 0x13FE)]
    public partial class KatanaOfKieri : BaseSword, IGMItem
    {
        public override int DefaultStrengthReq => 75;

        public override int DefaultMinDamage => 31;

        public override int DefaultMaxDamage => 46;
        
        public override int DefaultHitSound => 0x237;
        
        public override int DefaultMissSound => 0x232;
        
        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Slash2H;

        public override int DefaultSpeed => 70;
        
        public override int InitMinHits => 200;

        public override int InitMaxHits => 200;

        public override string DefaultName => "Katana of Kieri";

        [Constructible]
        public KatanaOfKieri() : base(0x13FE)
        {
            Slayer = CreatureType.Undead;
            Weight = 6.0;
            Hue = 1163;
            Identified = false;
        }
    }
}