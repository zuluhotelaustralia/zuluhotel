using System;
using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13B2, 0x13B1)]
    public partial class AirElementBow : BaseRanged, IGMItem
    {
        public override int EffectId { get; set; } = 0xF42;

        public override Type AmmoType => typeof(Arrow);

        public override Item Ammo => new Arrow();

        public override int DefaultStrengthReq => 110;

        public override int DefaultMinDamage => 32;

        public override int DefaultMaxDamage => 44;

        public override int DefaultSpeed => 53;

        public override int DefaultMaxRange => 12;

        public override int InitMinHits => 65;

        public override int InitMaxHits => 65;

        public override WeaponAnimation DefaultAnimation { get; } = WeaponAnimation.ShootBow;

        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Bow of the Air Element";

        [Constructible]
        public AirElementBow() : base(0x13B2)
        {
            Hue = 1161;
            Weight = 6.0;
            Layer = Layer.TwoHanded;
            DexBonus = 5;
        }
    }
}