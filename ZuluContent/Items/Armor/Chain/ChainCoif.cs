using ZuluContent.Zulu.Engines.Magic;

namespace Server.Items
{
    [FlipableAttribute(0x13BB, 0x13C0)]
    public class ChainCoif : BaseArmor, IFortifiable
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 20;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 8.0;

        public override int ArmorBase => 23;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Chainmail;


        [Constructible]
        public ChainCoif() : base(0x13BB)
        {
            Weight = 1.0;
        }

        [Constructible]
        public ChainCoif(Serial serial) : base(serial)
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