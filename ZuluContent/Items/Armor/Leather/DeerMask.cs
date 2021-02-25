namespace Server.Items
{
    public class DeerMask : BaseArmor
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 13;

        public override int DefaultStrReq => 25;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 4.0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override CraftResource DefaultResource => CraftResource.RegularLeather;


        [Constructible]
        public DeerMask() : base(0x1547)
        {
            Weight = 4.0;
        }

        [Constructible]
        public DeerMask(Serial serial) : base(serial)
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