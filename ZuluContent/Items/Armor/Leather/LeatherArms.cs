namespace Server.Items
{
    [FlipableAttribute(0x13cd, 0x13c5)]
    public class LeatherArms : BaseArmor
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 13;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 2.0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override CraftResource DefaultResource => CraftResource.RegularLeather;


        [Constructible]
        public LeatherArms() : base(0x13CD)
        {
            Weight = 2.0;
        }

        [Constructible]
        public LeatherArms(Serial serial) : base(serial)
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