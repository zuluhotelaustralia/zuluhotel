using ModernUO.Serialization;
using Server.Spells;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13FF, 0x13FE)]
    public partial class RaptsSavageKatanaOfChaos : BaseSword, IGMItem
    {
        public override int DefaultStrengthReq => 120;

        public override int DefaultMinDamage => 28;

        public override int DefaultMaxDamage => 50;

        public override int DefaultSpeed => 60;

        public override int InitMinHits => 200;

        public override int InitMaxHits => 200;

        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Rapt's Savage Katana of Chaos";

        [Constructible]
        public RaptsSavageKatanaOfChaos() : base(0x13FE)
        {
            SpellHitEntry = SpellEntry.ChaoticFlux;
            SpellHitChance = 0.3;
            Weight = 5.0;
            Hue = 1172;
            Identified = false;
        }
    }
}