using System;
using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13B2, 0x13B1)]
    public partial class BowOfTheWoodlandGaladriel : BaseRanged, IGMItem
    {
        public override int EffectId => 0xF42;

        public override Type AmmoType => typeof(Arrow);

        public override Item Ammo => new Arrow();

        public override int DefaultStrengthReq => 110;

        public override int DefaultMinDamage => 45;

        public override int DefaultMaxDamage => 70;

        public override int DefaultSpeed => 40;

        public override int DefaultMaxRange => 14;

        public override int InitMinHits => 200;

        public override int InitMaxHits => 200;

        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Bow of the Woodland Galadriel";

        [Constructible]
        public BowOfTheWoodlandGaladriel() : base(0x13B2)
        {
            Hue = 871;
            Identified = false;
            Weight = 9.0;
            Layer = Layer.OneHanded;
        }
    }
}