using System;

namespace Server.Items
{
    [FlipableAttribute(0x13FD, 0x13FC)]
    public class HeavyCrossbow : BaseRanged
    {
        public override int EffectId => 0x1BFE;

        public override Type AmmoType => typeof(Bolt);

        public override Item Ammo => new Bolt();

        public override int DefaultStrengthReq => 40;

        public override int DefaultMinDamage => 12;

        public override int DefaultMaxDamage => 32;

        public override int DefaultSpeed => 25;

        public override int DefaultMaxRange => 5;

        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        [Constructible]
        public HeavyCrossbow() : base(0x13FD)
        {
            Weight = 9.0;
            Layer = Layer.TwoHanded;
        }

        [Constructible]
        public HeavyCrossbow(Serial serial) : base(serial)
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
        }
    }
}