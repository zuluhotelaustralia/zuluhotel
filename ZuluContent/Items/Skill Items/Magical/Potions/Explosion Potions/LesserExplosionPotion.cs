namespace Server.Items
{
    public class LesserExplosionPotion : BaseExplosionPotion
    {
        public override uint PotionStrength { get; set; } = 1;
        
        [Constructible]
        public LesserExplosionPotion() : base(PotionEffect.ExplosionLesser)
        {
        }

        [Constructible]
        public LesserExplosionPotion(Serial serial) : base(serial)
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