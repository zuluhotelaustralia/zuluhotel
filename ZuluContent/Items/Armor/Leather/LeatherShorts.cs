namespace Server.Items
{
    [FlipableAttribute(0x1c00, 0x1c01)]
    public class LeatherShorts : BaseArmor
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 13;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 2.0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override CraftResource DefaultResource => CraftResource.RegularLeather;


        [Constructible]
        public LeatherShorts() : base(0x1C00)
        {
            Weight = 3.0;
        }

        [Constructible]
        public LeatherShorts(Serial serial) : base(serial)
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