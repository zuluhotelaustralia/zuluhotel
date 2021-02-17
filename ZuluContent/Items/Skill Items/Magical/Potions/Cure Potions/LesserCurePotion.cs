namespace Server.Items
{
    public class LesserCurePotion : BaseCurePotion
    {
        public override uint PotionStrength { get; set; } = 1;

        public override CureLevelInfo[] LevelInfo { get; } = {
            new(Poison.Lesser, 0.75), // 75% chance to cure lesser poison
            new(Poison.Regular, 0.50), // 50% chance to cure regular poison
            new(Poison.Greater, 0.15) // 15% chance to cure greater poison
        };

        [Constructible]
        public LesserCurePotion() : base(PotionEffect.CureLesser)
        {
        }

        [Constructible]
        public LesserCurePotion(Serial serial) : base(serial)
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