namespace Server.Items
{
    public class PlateGorget : BaseArmor
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 0;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 3.0;

        public override int ArmorBase => 30;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;


        [Constructible]
        public PlateGorget() : base(0x1413)
        {
            Weight = 2.0;
        }

        [Constructible]
        public PlateGorget(Serial serial) : base(serial)
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
        }
    }
}