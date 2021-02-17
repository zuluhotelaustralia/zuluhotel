namespace Server.Items
{
    public class GreaterHealPotion : BaseHealPotion
    {
        public override int MinHeal { get; } = 9;
        public override int MaxHeal { get; } = 30;
        public override double Delay { get; } = 10.0;
        public override uint PotionStrength { get; set; } = 5;

        [Constructible]
        public GreaterHealPotion() : base(PotionEffect.HealGreater)
        {
        }

        [Constructible]
        public GreaterHealPotion(Serial serial) : base(serial)
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