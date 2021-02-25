namespace Server.Items
{
    [FlipableAttribute(0x1c04, 0x1c05)]
    public class FemalePlateChest : BaseArmor
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 80;

        public override int DefaultDexBonus => -6;

        public override double DefaultMagicEfficiencyPenalty => 17.0;

        public override int ArmorBase => 25;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;


        [Constructible]
        public FemalePlateChest() : base(0x1C04)
        {
            Weight = 4.0;
        }

        [Constructible]
        public FemalePlateChest(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            if (Weight == 1.0)
                Weight = 4.0;
        }
    }
}