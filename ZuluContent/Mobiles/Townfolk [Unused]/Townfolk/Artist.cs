using Server.Items;

namespace Server.Mobiles
{
    public class Artist : BaseCreature
    {
        public override bool CanTeach
        {
            get { return true; }
        }


        [Constructible]
        public Artist()
            : base(AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4)
        {
            InitStats(31, 41, 51);

            SetSkill(SkillName.Healing, 36, 68);


            SpeechHue = Utility.RandomDyedHue();
            Title = "the artist";
            Hue = Race.RandomSkinHue();


            if (this.Female = Utility.RandomBool())
            {
                this.Body = 0x191;
                this.Name = NameList.RandomName("female");
            }
            else
            {
                this.Body = 0x190;
                this.Name = NameList.RandomName("male");
            }

            AddItem(new Doublet(Utility.RandomDyedHue()));
            AddItem(new Sandals());
            AddItem(new ShortPants(Utility.RandomNeutralHue()));
            AddItem(new HalfApron(Utility.RandomDyedHue()));

            Utility.AssignRandomHair(this);

            Container pack = new Backpack();

            pack.DropItem(new Gold(250, 300));

            pack.Movable = false;

            AddItem(pack);
        }

        public override bool ClickTitle
        {
            get { return false; }
        }


        [Constructible]
        public Artist(Serial serial)
            : base(serial)
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