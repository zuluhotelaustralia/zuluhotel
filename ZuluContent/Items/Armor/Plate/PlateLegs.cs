namespace Server.Items
{
    [FlipableAttribute(0x1411, 0x141a)]
    public class PlateLegs : BaseArmor
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 85;

        public override int DefaultDexBonus => -3;

        public override double DefaultMagicEfficiencyPenalty => 7.0;

        public override int ArmorBase => 30;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;


        [Constructible]
        public PlateLegs() : base(0x1411)
        {
            Weight = 7.0;
        }

        [Constructible]
        public PlateLegs(Serial serial) : base(serial)
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