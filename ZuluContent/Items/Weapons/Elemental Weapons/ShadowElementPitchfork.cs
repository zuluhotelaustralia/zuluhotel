using ModernUO.Serialization;
using Server.Engines.Magic.HitScripts;
using Server.Mobiles;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0xE87, 0xE88)]
    public partial class ShadowElementPitchfork : BaseSpear, IGMItem
    {
        public override int DefaultMaxRange => 2;

        public override int DefaultStrengthReq => 110;

        public override int DefaultMinDamage => 30;

        public override int DefaultMaxDamage => 45;

        public override int DefaultSpeed => 45;

        public override int DefaultHitSound => 0x23C;

        public override int DefaultMissSound => 0x23A;

        public override int InitMinHits => 80;

        public override int InitMaxHits => 80;

        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Pierce2H;

        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Pitchfork of the Shadow Element";

        [Constructible]
        public ShadowElementPitchfork() : base(0xE87)
        {
            Weight = 9.0;
            Hue = 1157;
        }
    }
}