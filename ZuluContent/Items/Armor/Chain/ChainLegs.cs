namespace Server.Items
{
    [FlipableAttribute(0x13be, 0x13c3)]
    public class ChainLegs : BaseArmor
    {
        public override int InitMinHits => 90;

        public override int InitMaxHits => 90;

        public override int DefaultStrReq => 20;

        public override int DefaultDexBonus => -3;

        public override double DefaultMagicEfficiencyPenalty => 8.0;

        public override int ArmorBase => 23;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Chainmail;


        [Constructible]
        public ChainLegs() : base(0x13BE)
        {
            Weight = 7.0;
        }

        [Constructible]
        public ChainLegs(Serial serial) : base(serial)
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