namespace Server.Items
{
    public class OrcHelm : BaseArmor
    {
        public override int InitMinHits => 40;

        public override int InitMaxHits => 40;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 5.0;

        public override int ArmorBase => 18;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Bone;


        [Constructible]
        public OrcHelm() : base(0x1F0B)
        {
        }

        [Constructible]
        public OrcHelm(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 1);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            if (version == 0 && (Weight == 1 || Weight == 5))
            {
                Weight = -1;
            }
        }
    }
}