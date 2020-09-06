using System;

namespace Server.Items
{
    [FlipableAttribute(0x13B2, 0x13B1)]
    public class Bow : BaseRanged
    {
        public override int EffectID { get; set; } = 0xF42;

        public override Type AmmoType
        {
            get { return typeof(Arrow); }
        }

        public override Item Ammo
        {
            get { return new Arrow(); }
        }

        public override int DefaultStrengthReq
        {
            get { return 20; }
        }

        public override int DefaultMinDamage
        {
            get { return 9; }
        }

        public override int DefaultMaxDamage
        {
            get { return 41; }
        }

        public override int DefaultSpeed
        {
            get { return 20; }
        }

        public override int DefaultMaxRange
        {
            get { return 10; }
        }

        public override int InitMinHits
        {
            get { return 31; }
        }

        public override int InitMaxHits
        {
            get { return 60; }
        }

        public override WeaponAnimation DefaultAnimation
        {
            get { return WeaponAnimation.ShootBow; }
        }


        [Constructible]
public Bow() : base(0x13B2)
        {
            Weight = 6.0;
            Layer = Layer.TwoHanded;
        }

        [Constructible]
public Bow(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (Weight == 7.0)
                Weight = 6.0;
        }
    }
}
