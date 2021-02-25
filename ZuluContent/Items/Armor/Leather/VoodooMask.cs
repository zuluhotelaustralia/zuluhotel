namespace Server.Items
{
    public class VoodooMask : BaseArmor
    {
        public override int InitMinHits => 65;

        public override int InitMaxHits => 65;

        public override int ArmorBase => 15;

        public override int DefaultStrReq => 40;

        public override int DefaultDexBonus => -6;

        public override double DefaultMagicEfficiencyPenalty => 1.0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override CraftResource DefaultResource => CraftResource.RegularLeather;


        [Constructible]
        public VoodooMask() : base(0x154B)
        {
            Weight = 2.0;
        }

        [Constructible]
        public VoodooMask(Serial serial) : base(serial)
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

            /*if ( Hue != 0 && (Hue < 2101 || Hue > 2130) )
				Hue = GetRandomHue();*/
        }
    }
}