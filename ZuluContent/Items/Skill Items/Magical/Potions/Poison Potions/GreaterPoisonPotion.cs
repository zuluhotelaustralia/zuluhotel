namespace Server.Items
{
    public class GreaterPoisonPotion : BasePoisonPotion
    {
        public override uint PotionStrength { get; set; } = 3;
        public override Poison Poison => Poison.Greater;
        public override double MinPoisoningSkill => 60.0;
        public override double MaxPoisoningSkill => 100.0;


        [Constructible]
        public GreaterPoisonPotion() : base(PotionEffect.PoisonGreater)
        {
        }

        [Constructible]
        public GreaterPoisonPotion(Serial serial) : base(serial)
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