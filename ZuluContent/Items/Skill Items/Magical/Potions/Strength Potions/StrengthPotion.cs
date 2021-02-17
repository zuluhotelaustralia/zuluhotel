using System;

namespace Server.Items
{
    public class StrengthPotion : BaseStrengthPotion
    {
        public override uint PotionStrength { get; set; } = 3;

        public override int StrOffset { get; } = 10;
        public override TimeSpan Duration { get; } = TimeSpan.FromMinutes(2.0);


        [Constructible]
        public StrengthPotion() : base(PotionEffect.Strength)
        {
        }

        [Constructible]
        public StrengthPotion(Serial serial) : base(serial)
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