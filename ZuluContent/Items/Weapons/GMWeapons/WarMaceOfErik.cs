using ModernUO.Serialization;
using Server.Engines.Magic.HitScripts;
using Server.Mobiles;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1407, 0x1406)]
    public partial class WarMaceOfErik : BaseBashing, IGMItem
    {
        public override int DefaultStrengthReq => 80;

        public override int DefaultMinDamage => 29;

        public override int DefaultMaxDamage => 49;

        public override int DefaultSpeed => 40;

        public override int InitMinHits => 200;

        public override int InitMaxHits => 200;

        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Erik's Big Basher";

        [Constructible]
        public WarMaceOfErik() : base(0x1406)
        {
            Weight = 17.0;
            Hue = 1175;
            Identified = false;
        }
    }
}