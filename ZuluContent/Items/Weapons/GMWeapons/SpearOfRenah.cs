using ModernUO.Serialization;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1403, 0x1402)]
    public partial class SpearOfRenah : BaseSpear, IGMItem
    {
        public override int DefaultStrengthReq => 80;

        public override int DefaultMinDamage => 31;

        public override int DefaultMaxDamage => 46;

        public override int DefaultSpeed => 65;

        public override int DefaultHitSound => 0x23C;

        public override int DefaultMissSound => 0x23A;

        public override int InitMinHits => 200;

        public override int InitMaxHits => 200;

        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Pierce1H;

        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Spear of Renah";

        [Constructible]
        public SpearOfRenah() : base(0x1402)
        {
            Weight = 4.0;
            Hue = 1181;
            Identified = false;
        }
    }
}