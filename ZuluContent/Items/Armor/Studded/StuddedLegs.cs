namespace Server.Items
{
    [FlipableAttribute(0x13da, 0x13e1)]
    public class StuddedLegs : BaseArmor
    {
        public override int InitMinHits => 80;

        public override int InitMaxHits => 80;

        public override int ArmorBase => 16;

        public override int DefaultStrReq => 35;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 3.0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;

        public override CraftResource DefaultResource => CraftResource.RegularLeather;


        [Constructible]
        public StuddedLegs() : base(0x13DA)
        {
            Weight = 5.0;
        }

        [Constructible]
        public StuddedLegs(Serial serial) : base(serial)
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

            if (Weight == 3.0)
                Weight = 5.0;
        }
    }
}