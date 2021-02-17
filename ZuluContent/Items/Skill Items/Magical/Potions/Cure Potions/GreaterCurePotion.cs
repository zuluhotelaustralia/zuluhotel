namespace Server.Items
{
    public class GreaterCurePotion : BaseCurePotion
    {
        public override uint PotionStrength { get; set; } = 5;
        
        public override CureLevelInfo[] LevelInfo { get; } = {
            new(Poison.Lesser, 1.00), // 100% chance to cure lesser poison
            new(Poison.Regular, 1.00), // 100% chance to cure regular poison
            new(Poison.Greater, 1.00), // 100% chance to cure greater poison
            new(Poison.Deadly, 0.75), //  75% chance to cure deadly poison
            new(Poison.Lethal, 0.25) //  25% chance to cure lethal poison
        };

        [Constructible]
        public GreaterCurePotion() : base(PotionEffect.CureGreater)
        {
        }

        [Constructible]
        public GreaterCurePotion(Serial serial) : base(serial)
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