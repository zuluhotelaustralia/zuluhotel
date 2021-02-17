namespace Server.Items
{
    public class GreaterExplosionPotion : BaseExplosionPotion
    {
        public override uint PotionStrength { get; set; } = 5;

        [Constructible]
        public GreaterExplosionPotion() : base(PotionEffect.ExplosionGreater)
        {
        }

        [Constructible]
        public GreaterExplosionPotion(Serial serial) : base(serial)
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