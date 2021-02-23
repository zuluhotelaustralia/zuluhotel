namespace Server.Items
{
    public class MetalKiteShield : BaseShield, IDyable
    {
        public override int InitMinHits => 105;

        public override int InitMaxHits => 105;

        public override int ArmorBase => 16;

        public override int DefaultStrReq => 20;

        public override int DefaultDexBonus => -4;

        public override double DefaultMagicEfficiencyPenalty => 16.0;


        [Constructible]
        public MetalKiteShield() : base(0x1B74)
        {
            Weight = 7.0;
        }

        [Constructible]
        public MetalKiteShield(Serial serial) : base(serial)
        {
        }

        public bool Dye(Mobile from, DyeTub sender)
        {
            if (Deleted)
                return false;

            Hue = sender.DyedHue;

            return true;
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (Weight == 5.0)
                Weight = 7.0;
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); //version
        }
    }
}