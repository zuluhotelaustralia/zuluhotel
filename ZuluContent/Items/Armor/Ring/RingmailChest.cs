namespace Server.Items
{
    [FlipableAttribute(0x13ec, 0x13ed)]
    public class RingmailChest : BaseArmor
    {
        public override int InitMinHits => 75;

        public override int InitMaxHits => 75;

        public override int DefaultStrReq => 20;

        public override int DefaultDexBonus => -5;

        public override double DefaultMagicEfficiencyPenalty => 13.0;

        public override int ArmorBase => 20;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Ringmail;


        [Constructible]
        public RingmailChest() : base(0x13EC)
        {
            Weight = 15.0;
        }

        [Constructible]
        public RingmailChest(Serial serial) : base(serial)
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

            if (Weight == 1.0)
                Weight = 15.0;
        }
    }
}