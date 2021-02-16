namespace Server.Items
{
    [FlipableAttribute(0x1415, 0x1416)]
    public class PlateChest : BaseArmor
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 90;

        public override int DefaultDexBonus => -9;

        public override double DefaultMagicEfficiencyPenalty => 22.0;

        public override int ArmorBase => 30;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;


        [Constructible]
        public PlateChest() : base(0x1415)
        {
            Weight = 10.0;
        }

        [Constructible]
        public PlateChest(Serial serial) : base(serial)
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
                Weight = 10.0;
        }
    }
}