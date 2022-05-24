using ModernUO.Serialization;
using Server.Engines.Magic.HitScripts;
using Server.Mobiles;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1405, 0x1404)]
    public partial class WaterElementFork : BaseSpear, IGMItem
    {
        public override int DefaultStrengthReq => 30;

        public override int DefaultMinDamage => 28;

        public override int DefaultMaxDamage => 49;

        public override int DefaultSpeed => 45;

        public override int DefaultHitSound => 0x23C;

        public override int DefaultMissSound => 0x23A;

        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Pierce1H;

        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "War Fork of the Water Element";

        [Constructible]
        public WaterElementFork() : base(0x1405)
        {
            Weight = 9.0;
            Hue = 1167;
        }
    }
}