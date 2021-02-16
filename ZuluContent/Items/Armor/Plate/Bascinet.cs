namespace Server.Items
{
    public class Bascinet : BaseArmor
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 10;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 4.0;

        public override int ArmorBase => 18;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;


        [Constructible]
        public Bascinet() : base(0x140C)
        {
            Weight = 5.0;
        }

        [Constructible]
        public Bascinet(Serial serial) : base(serial)
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
                Weight = 5.0;
        }
    }
}