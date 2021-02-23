namespace Server.Items
{
    public class BronzeShield : BaseShield
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 10;

        public override int DefaultStrReq => 20;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 10.0;


        [Constructible]
        public BronzeShield() : base(0x1B72)
        {
            Weight = 6.0;
        }

        [Constructible]
        public BronzeShield(Serial serial) : base(serial)
        {
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); //version
        }
    }
}