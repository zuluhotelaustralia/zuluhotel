namespace Server.Mobiles
{
    public class GypsyAnimalTrainer : AnimalTrainer
    {
        [Constructible]
        public GypsyAnimalTrainer()
        {
            if (Utility.RandomBool())
                Title = "the Gypsy Animal Trainer";
            else
                Title = "the Gypsy Animal Herder";
        }

        public override VendorShoeType ShoeType => Female ? VendorShoeType.ThighBoots : VendorShoeType.Boots;

        public override int GetShoeHue()
        {
            return 0;
        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            var item = FindItemOnLayer(Layer.Pants);

            if (item != null)
                item.Hue = Utility.RandomBrightHue();

            item = FindItemOnLayer(Layer.OuterLegs);

            if (item != null)
                item.Hue = Utility.RandomBrightHue();

            item = FindItemOnLayer(Layer.InnerLegs);

            if (item != null)
                item.Hue = Utility.RandomBrightHue();

            item = FindItemOnLayer(Layer.OuterTorso);

            if (item != null)
                item.Hue = Utility.RandomBrightHue();

            item = FindItemOnLayer(Layer.InnerTorso);

            if (item != null)
                item.Hue = Utility.RandomBrightHue();

            item = FindItemOnLayer(Layer.Shirt);

            if (item != null)
                item.Hue = Utility.RandomBrightHue();
        }

        [Constructible]
        public GypsyAnimalTrainer(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
        }
    }
}