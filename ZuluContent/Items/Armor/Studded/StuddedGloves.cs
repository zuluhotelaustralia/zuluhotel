namespace Server.Items
{
    [FlipableAttribute(0x13d5, 0x13dd)]
    public class StuddedGloves : BaseArmor
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 16;

        public override int DefaultStrReq => 25;

        public override double DefaultMagicEfficiencyPenalty => 1.0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;

        public override CraftResource DefaultResource => CraftResource.RegularLeather;


        [Constructible]
        public StuddedGloves() : base(0x13D5)
        {
            Weight = 1.0;
        }

        [Constructible]
        public StuddedGloves(Serial serial) : base(serial)
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

            if (Weight == 2.0)
                Weight = 1.0;
        }
    }
}