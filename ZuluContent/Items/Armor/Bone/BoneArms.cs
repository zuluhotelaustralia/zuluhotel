namespace Server.Items
{
    [FlipableAttribute(0x144e, 0x1453)]
    public class BoneArms : BaseArmor
    {
        public override int InitMinHits => 20;

        public override int InitMaxHits => 20;

        public override int DefaultStrReq => 40;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 10.0;

        public override int ArmorBase => 20;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Bone;


        [Constructible]
        public BoneArms() : base(0x144E)
        {
            Weight = 2.0;
        }

        [Constructible]
        public BoneArms(Serial serial) : base(serial)
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