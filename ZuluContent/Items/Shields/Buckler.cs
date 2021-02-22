namespace Server.Items
{
    public class Buckler : BaseShield
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 7;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 7.0;


        [Constructible]
        public Buckler() : base(0x1B73)
        {
            Weight = 5.0;
        }

        [Constructible]
        public Buckler(Serial serial) : base(serial)
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