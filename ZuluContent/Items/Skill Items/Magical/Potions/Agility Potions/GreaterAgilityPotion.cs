using System;

namespace Server.Items
{
    public class GreaterAgilityPotion : BaseAgilityPotion
    {
        public override uint PotionStrength { get; set; } = 3;

        public override int DexOffset { get; } = 20;
        public override TimeSpan Duration { get; } = TimeSpan.FromMinutes(2.0);


        [Constructible]
        public GreaterAgilityPotion() : base(PotionEffect.AgilityGreater)
        {
        }

        [Constructible]
        public GreaterAgilityPotion(Serial serial) : base(serial)
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