using Server.Items;

namespace Server.Mobiles
{
    public class Peasant : BaseEscortable
    {
        [Constructible]
        public Peasant()
        {
            Title = "the peasant";
        }

        public override bool CanTeach
        {
            get { return true; }
        }

        public override bool ClickTitle
        {
            get { return false; }
        } // Do not display 'the peasant' when single-clicking

        private static int GetRandomHue()
        {
            switch (Utility.Random(6))
            {
                default:
                case 0: return 0;
                case 1: return Utility.RandomBlueHue();
                case 2: return Utility.RandomGreenHue();
                case 3: return Utility.RandomRedHue();
                case 4: return Utility.RandomYellowHue();
                case 5: return Utility.RandomNeutralHue();
            }
        }

        public override void InitOutfit()
        {
            if (Female)
                AddItem(new PlainDress());
            else
                AddItem(new Shirt(GetRandomHue()));

            int lowHue = GetRandomHue();

            AddItem(new ShortPants(lowHue));

            if (Female)
                AddItem(new Boots());
            else
                AddItem(new Shoes());

            //if ( !Female )
            //AddItem( new BodySash( lowHue ) );

            //AddItem( new Cloak( GetRandomHue() ) );

            //if ( !Female )
            //AddItem( new Longsword() );

            Utility.AssignRandomHair(this);

            PackGold(200, 250);
        }

        [Constructible]
        public Peasant(Serial serial) : base(serial)
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