using System;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x13B2, 0x13B1)]
    public partial class FireBow : BaseRanged
    {
        public override string DefaultName => "Fire Bow";
        
        public override int EffectId => 0x36D4;
        
        public override int DefaultHitSound => 0x15E;

        public override Type AmmoType => typeof(FireArrow);

        public override Item Ammo => new FireArrow();

        public override int DefaultStrengthReq => 60;

        public override int DefaultMinDamage => 10;

        public override int DefaultMaxDamage => 35;

        public override int DefaultSpeed => 35;

        public override int DefaultMaxRange => 7;

        public override int InitMinHits => 65;

        public override int InitMaxHits => 65;

        public override WeaponAnimation DefaultAnimation => WeaponAnimation.ShootBow;

        [Constructible]
        public FireBow() : base(0x13B2)
        {
            Weight = 6.0;
            Layer = Layer.TwoHanded;
            Hue = 0x0494;
        }
    }
}