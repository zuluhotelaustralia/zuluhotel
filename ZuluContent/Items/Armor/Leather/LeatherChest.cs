namespace Server.Items
{
    [FlipableAttribute(0x13cc, 0x13d3)]
    public class LeatherChest : BaseArmor
    {
        public override int InitMinHits => 90;

        public override int InitMaxHits => 90;

        public override int ArmorBase => 13;

        public override int DefaultDexBonus => -3;

        public override double DefaultMagicEfficiencyPenalty => 3.0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override CraftResource DefaultResource => CraftResource.RegularLeather;


        [Constructible]
        public LeatherChest() : base(0x13CC)
        {
            Weight = 6.0;
        }

        [Constructible]
        public LeatherChest(Serial serial) : base(serial)
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