namespace Server.Items
{
    public class LesserHealPotion : BaseHealPotion
    {
        public override int MinHeal { get; } = 3;

        public override int MaxHeal { get; } = 10;

        public override double Delay { get; } = 10.0;


        [Constructible]
        public LesserHealPotion() : base(PotionEffect.HealLesser)
        {
        }

        [Constructible]
        public LesserHealPotion(Serial serial) : base(serial)
        {
        }

        public override uint PotionStrength { get; set; } = 1;

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