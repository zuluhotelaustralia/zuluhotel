using ModernUO.Serialization;
using ZuluContent.Zulu.Items;
namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1401, 0x1400)]
    public partial class PoisonElementKryss : BaseSword, IGMItem
    {
        public override int DefaultStrengthReq => 110;

        public override int DefaultMinDamage => 20;

        public override int DefaultMaxDamage => 26;

        public override int DefaultSpeed => 98;

        public override int DefaultHitSound => 0x23C;

        public override int DefaultMissSound => 0x23A;

        public override int InitMinHits => 90;

        public override int InitMaxHits => 90;

        public override SkillName DefaultSkill => SkillName.Fencing;

        public override WeaponType DefaultWeaponType => WeaponType.Piercing;

        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Pierce1H;

        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Kryss of the Poison Element";

        [Constructible]
        public PoisonElementKryss() : base(0x1401)
        {
            Weight = 2.0;
            Hue = 264;
            Poison = Poison.Greater;
            PermaPoison = true;
        }
    }
}