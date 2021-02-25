using Server.Items;

namespace Server.Mobiles
{
    public class BrideGroom : BaseEscortable
    {
        [Constructible]
        public BrideGroom()
        {
            if (Female)
                Title = "the bride";
            else
                Title = "the groom";
        }

        public override bool CanTeach
        {
            get { return true; }
        }

        public override bool ClickTitle
        {
            get { return false; }
        } // Do not display 'the groom' when single-clicking

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
                AddItem(new FancyDress());
            else
                AddItem(new FancyShirt());

            int lowHue = GetRandomHue();

            AddItem(new LongPants(lowHue));

            if (Female)
                AddItem(new Shoes());
            else
                AddItem(new Boots());

            if (Utility.RandomBool())
                HairItemID = 0x203B;
            else
                HairItemID = 0x203C;

            HairHue = this.Race.RandomHairHue();

            PackGold(200, 250);
        }

        [Constructible]
        public BrideGroom(Serial serial) : base(serial)
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