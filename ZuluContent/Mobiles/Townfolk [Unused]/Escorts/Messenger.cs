using Server.Items;

namespace Server.Mobiles
{
    public class Messenger : BaseEscortable
    {
        [Constructible]
        public Messenger()
        {
            Title = "the messenger";
        }

        public override bool CanTeach
        {
            get { return true; }
        }

        public override bool ClickTitle
        {
            get { return false; }
        } // Do not display 'the messenger' when single-clicking

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

            switch (Utility.Random(4))
            {
                case 0:
                    AddItem(new ShortHair(Race.RandomHairHue()));
                    break;
                case 1:
                    AddItem(new TwoPigTails(Race.RandomHairHue()));
                    break;
                case 2:
                    AddItem(new ReceedingHair(Race.RandomHairHue()));
                    break;
                case 3:
                    AddItem(new KrisnaHair(Race.RandomHairHue()));
                    break;
            }

            PackGold(200, 250);
        }

        [Constructible]
        public Messenger(Serial serial) : base(serial)
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