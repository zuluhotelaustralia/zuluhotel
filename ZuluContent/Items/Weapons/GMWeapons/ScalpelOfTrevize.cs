using Server.Engines.Magic.HitScripts;
using Server.Mobiles;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0xEC4, 0xEC5)]
    public partial class ScalpelOfTrevize : BaseKnife, IGMItem
    {
        public override int DefaultStrengthReq => 30;

        public override int DefaultMinDamage => 22;

        public override int DefaultMaxDamage => 29;

        public override int DefaultSpeed => 60;
        
        public override int InitMinHits => 200;

        public override int InitMaxHits => 200;
        
        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Scalpel of Trevize";

        [Constructible]
        public ScalpelOfTrevize() : base(0x0EC4)
        {
            EffectHitType = EffectHitType.Piercing;
            EffectHitTypeChance = 1.0;
            Weight = 1.0;
            Hue = 1176;
            Identified = false;
        }
    }
}