using Server.Mobiles;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0xF62, 0xF63)]
    public partial class LanceOfLothian : BaseSpear, IGMItem
    {
        public override int DefaultMaxRange => 2;
        
        public override int DefaultStrengthReq => 85;

        public override int DefaultMinDamage => 37;

        public override int DefaultMaxDamage => 52;
        
        public override int DefaultHitSound => 0x23C;
        
        public override int DefaultMissSound => 0x23A;

        public override int DefaultSpeed => 50;
        
        public override int InitMinHits => 200;

        public override int InitMaxHits => 200;

        public override string DefaultName => "Lance of Lothian";

        [Constructible]
        public LanceOfLothian() : base(0xF63)
        {
            EffectHitType = EffectHitType.Banish;
            EffectHitTypeChance = 1.0;
            Weight = 7.0;
            Hue = 1171;
            Identified = false;
        }
    }
}