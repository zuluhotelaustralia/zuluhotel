using ModernUO.Serialization;
using Server.Spells;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13FF, 0x13FE)]
    public partial class JustaesAvengingKatanaOfJustice : BaseSword, IGMItem
    {
        public override int DefaultStrengthReq => 120;

        public override int DefaultMinDamage => 31;

        public override int DefaultMaxDamage => 53;

        public override int DefaultSpeed => 60;

        public override int InitMinHits => 200;

        public override int InitMaxHits => 200;

        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Justae's Avenging Katana of Justice";

        [Constructible]
        public JustaesAvengingKatanaOfJustice() : base(0x13FE)
        {
            SpellHitEntry = SpellEntry.UndeadBane;
            SpellHitChance = 0.3;
            Weight = 5.0;
            Hue = 1174;
            Identified = false;
        }
    }
}