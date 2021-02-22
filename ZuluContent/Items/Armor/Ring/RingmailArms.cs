namespace Server.Items
{
    [FlipableAttribute(0x13ee, 0x13ef)]
    public class RingmailArms : BaseArmor
    {
        public override int InitMinHits => 65;

        public override int InitMaxHits => 65;

        public override int DefaultStrReq => 20;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 6.0;

        public override int ArmorBase => 20;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Ringmail;


        [Constructible]
        public RingmailArms() : base(0x13EE)
        {
            Weight = 15.0;
        }

        [Constructible]
        public RingmailArms(Serial serial) : base(serial)
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