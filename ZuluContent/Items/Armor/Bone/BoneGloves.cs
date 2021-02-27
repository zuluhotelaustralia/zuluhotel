namespace Server.Items
{
    [FlipableAttribute(0x1450, 0x1455)]
    public class BoneGloves : BaseArmor
    {
        public override int InitMinHits => 20;

        public override int InitMaxHits => 20;

        public override int DefaultStrReq => 40;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 5.0;

        public override int ArmorBase => 20;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Bone;


        [Constructible]
        public BoneGloves() : base(0x1450)
        {
            Weight = 2.0;
        }

        [Constructible]
        public BoneGloves(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);

            if (Weight == 1.0)
                Weight = 2.0;
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}