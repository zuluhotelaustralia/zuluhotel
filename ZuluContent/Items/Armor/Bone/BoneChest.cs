namespace Server.Items
{
    [FlipableAttribute(0x144f, 0x1454)]
    public class BoneChest : BaseArmor
    {
        public override int InitMinHits => 20;

        public override int InitMaxHits => 20;

        public override int DefaultStrReq => 40;

        public override int DefaultDexBonus => -6;

        public override double DefaultMagicEfficiencyPenalty => 25.0;

        public override int ArmorBase => 20;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Bone;


        [Constructible]
        public BoneChest() : base(0x144F)
        {
            Weight = 6.0;
        }

        [Constructible]
        public BoneChest(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);

            if (Weight == 1.0)
                Weight = 6.0;
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}