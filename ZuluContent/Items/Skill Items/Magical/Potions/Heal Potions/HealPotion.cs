namespace Server.Items
{
    public class HealPotion : BaseHealPotion
    {
        public override int MinHeal { get; } = 6;
        public override int MaxHeal { get; } = 20;
        public override double Delay { get; } = 10.0;

        public override uint PotionStrength { get; set; } = 3;


        [Constructible]
        public HealPotion() : base(PotionEffect.Heal)
        {
        }

        [Constructible]
        public HealPotion(Serial serial) : base(serial)
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