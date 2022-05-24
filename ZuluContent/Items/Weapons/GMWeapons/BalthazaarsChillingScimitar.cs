using ModernUO.Serialization;
using Server.Spells;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13B6, 0x13B5)]
    public partial class BalthazaarsChillingScimitar : BaseSword, IGMItem
    {
        public override int DefaultStrengthReq => 60;

        public override int DefaultMinDamage => 17;

        public override int DefaultMaxDamage => 29;

        public override int DefaultSpeed => 50;

        public override int InitMinHits => 200;

        public override int InitMaxHits => 200;

        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Balthazaar's Chilling Scimitar";

        [Constructible]
        public BalthazaarsChillingScimitar() : base(0x13B5)
        {
            SpellHitEntry = SpellEntry.IceStrike;
            SpellHitChance = 0.3;
            Weight = 5.0;
            Hue = 1170;
            Identified = false;
        }
    }
}