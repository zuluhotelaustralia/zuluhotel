namespace Server.Items
{
    [FlipableAttribute(0x1c0a, 0x1c0b, 0x1C0A)]
    public class LeatherBustierArms : BaseArmor
    {
        public override int InitMinHits => 90;

        public override int InitMaxHits => 90;

        public override int ArmorBase => 13;

        public override int DefaultStrReq => 25;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 8.0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override CraftResource DefaultResource => CraftResource.RegularLeather;


        [Constructible]
        public LeatherBustierArms() : base(0x1C0A)
        {
            Weight = 1.0;
        }

        [Constructible]
        public LeatherBustierArms(Serial serial) : base(serial)
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