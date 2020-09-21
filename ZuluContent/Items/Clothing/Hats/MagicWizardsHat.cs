namespace Server.Items
{
    public class MagicWizardsHat : BaseHat
    {
        public override int InitMinHits
        {
            get { return 20; }
        }

        public override int InitMaxHits
        {
            get { return 30; }
        }

        public override int LabelNumber
        {
            get { return 1041072; }
        } // a magical wizard's hat

        public override int DefaultStrBonus
        {
            get { return -5; }
        }

        public override int DefaultDexBonus
        {
            get { return -5; }
        }

        public override int DefaultIntBonus
        {
            get { return +5; }
        }


        [Constructible]
        public MagicWizardsHat() : this(0)
        {
        }


        [Constructible]
        public MagicWizardsHat(int hue) : base(0x1718, hue)
        {
            Weight = 1.0;
        }

        [Constructible]
        public MagicWizardsHat(Serial serial) : base(serial)
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