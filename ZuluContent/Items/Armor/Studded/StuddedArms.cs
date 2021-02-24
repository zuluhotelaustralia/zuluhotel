namespace Server.Items
{
    [FlipableAttribute(0x13dc, 0x13d4)]
    public class StuddedArms : BaseArmor
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 16;

        public override int DefaultStrReq => 25;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 3.0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;

        public override CraftResource DefaultResource => CraftResource.RegularLeather;


        [Constructible]
        public StuddedArms() : base(0x13DC)
        {
            Weight = 4.0;
        }

        [Constructible]
        public StuddedArms(Serial serial) : base(serial)
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