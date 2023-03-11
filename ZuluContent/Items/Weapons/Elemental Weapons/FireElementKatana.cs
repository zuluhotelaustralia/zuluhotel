using ModernUO.Serialization;
using Server.Engines.Magic.HitScripts;
using Server.Mobiles;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;
using Server.Spells;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13FF, 0x13FE)]
    public partial class FireElementKatana : BaseSword, IGMItem
    {
        public override int DefaultStrengthReq => 110;

        public override int DefaultMinDamage => 26;

        public override int DefaultMaxDamage => 41;

        public override int DefaultSpeed => 87;

        public override int DefaultHitSound => 0x237;

        public override int DefaultMissSound => 0x232;

        public override int InitMinHits => 90;

        public override int InitMaxHits => 90;

        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Slash1H;

        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Katana of the Fire Element";

        [Constructible]
        public FireElementKatana() : base(0x13FE)
        {
            Weight = 6.0;
            Hue = 1172;
            SpellHitEntry = SpellEntry.Fireball;
            SpellHitChance = 0.3;
        }
    }
}