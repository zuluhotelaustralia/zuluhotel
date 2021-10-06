using Server.Mobiles;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x143E, 0x143F)]
    public partial class HalberdfOfAlcatraz : BasePoleArm, IGMItem
    {
        public override int DefaultStrengthReq => 105;

        public override int DefaultMinDamage => 50;

        public override int DefaultMaxDamage => 75;
        
        public override int DefaultHitSound => 0x237;

        public override int DefaultSpeed => 20;
        
        public override int InitMinHits => 200;

        public override int InitMaxHits => 200;
        
        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Halberd of Alcatraz";

        [Constructible]
        public HalberdfOfAlcatraz() : base(0x143E)
        {
            Weight = 16.0;
            Layer = Layer.TwoHanded;
            Hue = 1284;
            Identified = false;
        }
    }
}