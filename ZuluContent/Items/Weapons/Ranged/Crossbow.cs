using System;

namespace Server.Items
{
    [FlipableAttribute(0xF50, 0xF4F)]
    public class Crossbow : BaseRanged
    {
        public override int EffectId
        {
            get { return 0x1BFE; }
        }

        public override Type AmmoType => typeof(Bolt);

        public override Item Ammo => new Bolt();

        public override int DefaultStrengthReq { get; } = 30;

        public override int DefaultMinDamage { get; } = 8;

        public override int DefaultMaxDamage { get; } = 43;

        public override int DefaultSpeed { get; } = 18;

        public override int DefaultMaxRange { get; } = 8;

        public override int InitMinHits { get; } = 31;

        public override int InitMaxHits { get; } = 80;
        


        [Constructible]
        public Crossbow() : base(0xF50)
        {
            Weight = 7.0;
            Layer = Layer.TwoHanded;
        }

        [Constructible]
        public Crossbow(Serial serial) : base(serial)
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