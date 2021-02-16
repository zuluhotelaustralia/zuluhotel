namespace Server.Items
{
    public class CurePotion : BaseCurePotion
    {
        public override uint PotionStrength { get; set; } = 3;
        public override CureLevelInfo[] LevelInfo { get; } = {
            new(Poison.Lesser, 1.00), // 100% chance to cure lesser poison
            new(Poison.Regular, 0.75), //  75% chance to cure regular poison
            new(Poison.Greater, 0.50), //  50% chance to cure greater poison
            new(Poison.Deadly, 0.15) //  15% chance to cure deadly poison
        };


        [Constructible]
        public CurePotion() : base(PotionEffect.Cure)
        {
        }

        [Constructible]
        public CurePotion(Serial serial) : base(serial)
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