namespace Server.Items
{
    [FlipableAttribute(0x1c0c, 0x1c0d)]
    public class StuddedBustierArms : BaseArmor
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 16;

        public override int DefaultStrReq => 25;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 4.0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;

        public override CraftResource DefaultResource => CraftResource.RegularLeather;


        [Constructible]
        public StuddedBustierArms() : base(0x1C0C)
        {
            Weight = 1.0;
        }

        [Constructible]
        public StuddedBustierArms(Serial serial) : base(serial)
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