namespace Server.Items
{
    [FlipableAttribute(0x1c02, 0x1c03)]
    public class FemaleStuddedChest : BaseArmor
    {
        public override int InitMinHits => 80;

        public override int InitMaxHits => 80;

        public override int ArmorBase => 15;

        public override int DefaultStrReq => 35;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 8.0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;

        public override CraftResource DefaultResource => CraftResource.RegularLeather;


        [Constructible]
        public FemaleStuddedChest() : base(0x1C02)
        {
            Weight = 6.0;
        }

        [Constructible]
        public FemaleStuddedChest(Serial serial) : base(serial)
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
                Weight = 6.0;
        }
    }
}