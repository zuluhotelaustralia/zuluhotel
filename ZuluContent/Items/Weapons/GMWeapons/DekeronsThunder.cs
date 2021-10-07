using System;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x13FD, 0x13FC)]
    public partial class DekeronsThunder : BaseRanged, IGMItem
    {
        public override int EffectId => 0x379F;
        public override int DefaultHitSound => 0x206;

        public override Type AmmoType => typeof(ThunderBolt);

        public override Item Ammo => new ThunderBolt();

        public override int DefaultStrengthReq => 80;

        public override int DefaultMinDamage => 45;

        public override int DefaultMaxDamage => 70;

        public override int DefaultSpeed => 35;

        public override int DefaultMaxRange => 7;

        public override int InitMinHits => 200;

        public override int InitMaxHits => 200;
        
        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Dekeron's Thunder";

        [Constructible]
        public DekeronsThunder() : base(0x13FD)
        {
            Hue = 1282;
            Identified = false;
            Weight = 9.0;
            Layer = Layer.TwoHanded;
        }
    }
}