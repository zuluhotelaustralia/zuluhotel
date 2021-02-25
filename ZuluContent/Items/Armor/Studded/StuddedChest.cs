namespace Server.Items
{
    [FlipableAttribute(0x13db, 0x13e2)]
    public class StuddedChest : BaseArmor
    {
        public override int InitMinHits => 90;

        public override int InitMaxHits => 90;

        public override int ArmorBase => 16;

        public override int DefaultStrReq => 35;

        public override int DefaultDexBonus => -3;

        public override double DefaultMagicEfficiencyPenalty => 10.0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;

        public override CraftResource DefaultResource => CraftResource.RegularLeather;


        [Constructible]
        public StuddedChest() : base(0x13DB)
        {
            Weight = 8.0;
        }

        [Constructible]
        public StuddedChest(Serial serial) : base(serial)
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
                Weight = 8.0;
        }
    }
}