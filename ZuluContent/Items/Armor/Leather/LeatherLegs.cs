namespace Server.Items
{
    [FlipableAttribute(0x13cb, 0x13d2)]
    public class LeatherLegs : BaseArmor
    {
        public override int InitMinHits => 80;

        public override int InitMaxHits => 80;

        public override int ArmorBase => 13;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 2.0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override CraftResource DefaultResource => CraftResource.RegularLeather;


        [Constructible]
        public LeatherLegs() : base(0x13CB)
        {
            Weight = 4.0;
        }

        [Constructible]
        public LeatherLegs(Serial serial) : base(serial)
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