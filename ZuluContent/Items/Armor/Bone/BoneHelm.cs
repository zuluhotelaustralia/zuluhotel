using ZuluContent.Zulu.Engines.Magic;

namespace Server.Items
{
    [FlipableAttribute(0x1451, 0x1456)]
    public class BoneHelm : BaseArmor, IFortifiable
    {
        public override int InitMinHits => 20;

        public override int InitMaxHits => 20;

        public override int DefaultStrReq => 40;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 10.0;

        public override int ArmorBase => 20;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Bone;


        [Constructible]
        public BoneHelm() : base(0x1451)
        {
            Weight = 3.0;
        }

        [Constructible]
        public BoneHelm(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);

            if (Weight == 1.0)
                Weight = 3.0;
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}