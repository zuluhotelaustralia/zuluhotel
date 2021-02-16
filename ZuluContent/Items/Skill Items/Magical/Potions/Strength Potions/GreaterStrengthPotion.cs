using System;

namespace Server.Items
{
    public class GreaterStrengthPotion : BaseStrengthPotion
    {
        public override uint PotionStrength { get; set; } = 3;

        public override int StrOffset { get; } = 20;

        public override TimeSpan Duration { get; } = TimeSpan.FromMinutes(2.0);


        [Constructible]
        public GreaterStrengthPotion() : base(PotionEffect.StrengthGreater)
        {
        }

        [Constructible]
        public GreaterStrengthPotion(Serial serial) : base(serial)
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