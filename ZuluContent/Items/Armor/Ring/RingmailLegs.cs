namespace Server.Items
{
    [FlipableAttribute(0x13f0, 0x13f1)]
    public class RingmailLegs : BaseArmor
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 20;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 6.0;

        public override int ArmorBase => 20;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Ringmail;


        [Constructible]
        public RingmailLegs() : base(0x13F0)
        {
            Weight = 15.0;
        }

        [Constructible]
        public RingmailLegs(Serial serial) : base(serial)
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