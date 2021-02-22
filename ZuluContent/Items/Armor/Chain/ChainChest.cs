namespace Server.Items
{
    [FlipableAttribute(0x13bf, 0x13c4)]
    public class ChainChest : BaseArmor
    {
        public override int InitMinHits => 110;

        public override int InitMaxHits => 110;

        public override int DefaultStrReq => 20;

        public override int DefaultDexBonus => -5;

        public override double DefaultMagicEfficiencyPenalty => 20.0;

        public override int ArmorBase => 23;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Chainmail;


        [Constructible]
        public ChainChest() : base(0x13BF)
        {
            Weight = 7.0;
        }

        [Constructible]
        public ChainChest(Serial serial) : base(serial)
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