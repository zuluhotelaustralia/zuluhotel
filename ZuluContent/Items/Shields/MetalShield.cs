namespace Server.Items
{
    public class MetalShield : BaseShield
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 11;

        public override int DefaultStrReq => 20;

        public override int DefaultDexBonus => -3;

        public override double DefaultMagicEfficiencyPenalty => 11.0;


        [Constructible]
        public MetalShield() : base(0x1B7B)
        {
            Weight = 6.0;
        }

        [Constructible]
        public MetalShield(Serial serial) : base(serial)
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