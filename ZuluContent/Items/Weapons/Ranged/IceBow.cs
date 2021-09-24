using System;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x13B2, 0x13B1)]
    public partial class IceBow : BaseRanged
    {
        public override string DefaultName => "Ice Bow";
        
        public override int EffectId => 0x3818;

        public override Type AmmoType => typeof(IceArrow);

        public override Item Ammo => new IceArrow();

        public override int DefaultStrengthReq => 60;

        public override int DefaultMinDamage => 10;

        public override int DefaultMaxDamage => 35;

        public override int DefaultSpeed => 35;

        public override int DefaultMaxRange => 7;

        public override int InitMinHits => 65;

        public override int InitMaxHits => 65;

        public override WeaponAnimation DefaultAnimation => WeaponAnimation.ShootBow;

        public override void OnSingleClick(Mobile from)
        {
            LabelTo(from, Name);
        }

        [Constructible]
        public IceBow() : base(0x13B2)
        {
            Weight = 6.0;
            Layer = Layer.TwoHanded;
            Hue = 0x0492;
        }
    }
}