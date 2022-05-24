using ModernUO.Serialization;
using Server.Engines.Magic.HitScripts;
using Server.Mobiles;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0xF5C, 0xF5D)]
    public partial class AnubisMaceOfDeath : BaseBashing, IGMItem
    {
        public override int DefaultStrengthReq => 90;

        public override int DefaultMinDamage => 37;

        public override int DefaultMaxDamage => 49;

        public override int DefaultSpeed => 35;

        public override int InitMinHits => 200;

        public override int InitMaxHits => 200;

        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Anubis's Mace of Death";

        [Constructible]
        public AnubisMaceOfDeath() : base(0xF5C)
        {
            Weight = 14.0;
            Hue = 1172;
            Identified = false;
        }
    }
}