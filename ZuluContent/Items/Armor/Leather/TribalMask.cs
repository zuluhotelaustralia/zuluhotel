namespace Server.Items
{
    public class TribalMask : BaseArmor
    {
        public override int InitMinHits => 65;

        public override int InitMaxHits => 65;

        public override int ArmorBase => 15;

        public override int DefaultStrReq => 40;

        public override int DefaultDexBonus => -4;

        public override double DefaultMagicEfficiencyPenalty => 2.0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override CraftResource DefaultResource => CraftResource.RegularLeather;


        [Constructible]
        public TribalMask() : base(0x1549)
        {
            Weight = 2.0;
        }

        [Constructible]
        public TribalMask(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}