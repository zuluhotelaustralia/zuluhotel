namespace Server.Items
{
    public class MonkRobe : BaseOuterTorso
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        [Constructible]
        public MonkRobe() : this(0x21E)
        {
        }


        [Constructible]
        public MonkRobe(int hue) : base(0x2687, hue)
        {
            Weight = 1.0;
            StrRequirement = 0;
        }

        public override int LabelNumber
        {
            get { return 1076584; }
        } // A monk's robe

        public override bool CanBeBlessed
        {
            get { return false; }
        }

        public override bool Dye(Mobile from, DyeTub sender)
        {
            from.SendLocalizedMessage(sender.FailMessage);
            return false;
        }

        [Constructible]
        public MonkRobe(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}