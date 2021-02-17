namespace Server.Items
{
    [FlipableAttribute(0x1414, 0x1418)]
    public class PlateGloves : BaseArmor
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 40;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 4.0;

        public override int ArmorBase => 30;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;


        [Constructible]
        public PlateGloves() : base(0x1414)
        {
            Weight = 2.0;
        }

        [Constructible]
        public PlateGloves(Serial serial) : base(serial)
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
                Weight = 2.0;
        }
    }
}