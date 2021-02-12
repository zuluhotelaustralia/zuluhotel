namespace Server.Items
{
    public class GrandMageRefreshElixir : BasePotion
    {
        public override string DefaultName { get; } = "Grand Mage Refresh Elixir";

        public override int Hue { get; set; } = 0x486;

        public GrandMageRefreshElixir() : base(0xEFB, PotionEffect.GrandMageRefreshElixir)
        {
        }

        public GrandMageRefreshElixir(Serial serial) : base(serial)
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
        

        public override void Drink(Mobile from)
        {
            if (from.Mana == from.ManaMax)
            {
                from.SendLocalizedMessage(501846); // You are at peace.
                return;
            }

            from.Mana = from.ManaMax;
            PlayDrinkEffect(from);
            Consume();
        }
    }
}