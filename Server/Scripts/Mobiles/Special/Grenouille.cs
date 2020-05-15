using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    public class Grenouille : BaseCreature
    {
        [Constructable]
        public Grenouille() : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Title = "the squire";
            Name = "Grenouille";

            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();
            this.Body = 0x190;
            Container pack = new Backpack();
            pack.Movable = false;

            InitStats(200, 200, 200);
            SetSkill(SkillName.Parry, 150.0, 200.0);
            SetSkill(SkillName.Swords, 150.0, 200.0);
            SetSkill(SkillName.Tactics, 150.0, 200.0);
            Female = false;

            int greenhue = 73;
            int pinkhue = 23;

            AddItem(new FancyShirt(pinkhue));
            AddItem(new ShortPants(pinkhue));
            AddItem(new Boots(greenhue));
            AddItem(new BodySash(greenhue));
            AddItem(new Cloak(pinkhue));
            AddItem(new Longsword());
            AddItem(pack);
            Utility.AssignRandomHair(this);

        }

        public override bool IsInvulnerable { get { return true; } }
        public override bool CanTeach { get { return false; } }
        public override bool ClickTitle { get { return false; } }

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

        public Grenouille(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
