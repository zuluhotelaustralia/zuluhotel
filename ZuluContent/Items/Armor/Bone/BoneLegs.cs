namespace Server.Items
{
    [FlipableAttribute(0x1452, 0x1457)]
    public class BoneLegs : BaseArmor
    {
        public override int InitMinHits => 35;

        public override int InitMaxHits => 35;

        public override int DefaultStrReq => 40;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 10.0;

        public override int ArmorBase => 20;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Bone;


        [Constructible]
        public BoneLegs() : base(0x1452)
        {
            Weight = 3.0;
        }

        [Constructible]
        public BoneLegs(Serial serial) : base(serial)
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