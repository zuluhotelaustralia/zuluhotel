using ModernUO.Serialization;
using Server.Mobiles;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0xF4D, 0xF4E)]
    public partial class BardicheOfLynx : BasePoleArm, IGMItem
    {
        public override int DefaultStrengthReq => 95;

        public override int DefaultMinDamage => 44;

        public override int DefaultMaxDamage => 64;

        public override int DefaultSpeed => 25;

        public override int InitMinHits => 200;

        public override int InitMaxHits => 200;

        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Bardiche of Lynx";

        [Constructible]
        public BardicheOfLynx() : base(0xF4E)
        {
            Weight = 7.0;
            Layer = Layer.TwoHanded;
            Hue = 1293;
            Identified = false;
        }
    }
}