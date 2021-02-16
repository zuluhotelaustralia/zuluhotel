namespace Server.Items
{
    public class HeaterShield : BaseShield
    {
        public override int InitMinHits => 130;

        public override int InitMaxHits => 130;

        public override int ArmorBase => 16;

        public override int DefaultStrReq => 30;

        public override int DefaultDexBonus => -5;

        public override double DefaultMagicEfficiencyPenalty => 16.0;

        [Constructible]
        public HeaterShield() : base(0x1B76)
        {
            Weight = 8.0;
        }

        [Constructible]
        public HeaterShield(Serial serial) : base(serial)
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