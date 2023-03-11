using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1439, 0x1438)]
    public partial class EarthElementWarHammer : BaseBashing, IGMItem
    {
        public override int DefaultStrengthReq => 110;

        public override int DefaultMinDamage => 32;

        public override int DefaultMaxDamage => 52;

        public override int DefaultSpeed => 51;

        public override int DefaultHitSound => 0x13C;

        public override int DefaultMissSound => 0x234;

        public override int InitMinHits => 110;

        public override int InitMaxHits => 110;

        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Bash2H;

        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "War Hammer of the Earth Element";

        [Constructible]
        public EarthElementWarHammer() : base(0x1439)
        {
            Weight = 10.0;
            Hue = 1134;
        }
    }
}