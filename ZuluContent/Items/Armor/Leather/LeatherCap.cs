namespace Server.Items
{
    [FlipableAttribute(0x1db9, 0x1dba)]
    public class LeatherCap : BaseArmor
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 13;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 1.0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override CraftResource DefaultResource => CraftResource.RegularLeather;


        [Constructible]
        public LeatherCap() : base(0x1DB9)
        {
            Weight = 2.0;
        }

        [Constructible]
        public LeatherCap(Serial serial) : base(serial)
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