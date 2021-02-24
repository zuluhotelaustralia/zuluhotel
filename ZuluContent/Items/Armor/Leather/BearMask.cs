namespace Server.Items
{
    public class BearMask : BaseArmor
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 15;

        public override int DefaultStrReq => 20;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 5.0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override CraftResource DefaultResource => CraftResource.RegularLeather;

        [Constructible]
        public BearMask() : base(0x1545)
        {
            Weight = 5.0;
        }

        [Constructible]
        public BearMask(Serial serial) : base(serial)
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