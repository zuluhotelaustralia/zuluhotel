namespace Server.Items
{
    public class TamlaHealPotion : BasePotion
    {
        public override uint PotionStrength { get; set; } = 1;

        public override string DefaultName { get; } = "Tamla Heal Potion";

        public override int Hue { get; set; } = 155;

        public TamlaHealPotion() : base(0xF0B, PotionEffect.TamlaHeal)
        {
        }

        public TamlaHealPotion(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
        

        public override void Drink(Mobile from)
        {
            if (from.Hits == from.HitsMax)
            {
                from.SendLocalizedMessage(1049547); // You are already at full health.
                return;
            }

            from.FixedEffect(0x3769, 10, 15);
            from.PlaySound(0x202);
            from.Hits = from.HitsMax;
            PlayDrinkEffect(from);
            Consume();
        }
    }
}