using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("a sanguin conscript corpse")]
    public class SanguinConscript : BaseCreature
    {
        [Constructable]
        public SanguinConscript() : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Name = "Sanguin Conscript";
            Hue = Utility.RandomSkinHue();

            if (this.Female = Utility.RandomBool())
            {
                this.Body = 0x191;
            }
            else
            {
                this.Body = 0x190;
            }

            SetStr(60, 80);
            SetDex(40, 45);
            SetInt(20, 25);

            SetHits(60, 80);

            SetDamage(6, 10); //Uses Weapon

            VirtualArmor = 10;

            SetSkill(SkillName.Tactics, 100.0, 100.0); //Uses Weapon

            SetSkill(SkillName.Macing, 65.0, 70.0);
            SetSkill(SkillName.Fencing, 65.0, 70.0);
            SetSkill(SkillName.Swords, 65.0, 70.0);
            SetSkill(SkillName.Wrestling, 65.0, 70.0);

            SetSkill(SkillName.MagicResist, 30.0, 35.0);

            Fame = 1500;
            Karma = -1500;

            Item Sandals = new Sandals();
            Sandals.Movable = false;
            Sandals.Hue = 1775;
            AddItem(Sandals);

            Item BodySash = new BodySash();
            BodySash.Movable = false;
            BodySash.Hue = 2106;
            AddItem(BodySash);

            Item Kilt = new Kilt();
            Kilt.Movable = false;
            Kilt.Hue = 1775;
            AddItem(Kilt);

            Item StuddedChest = new StuddedChest();
            StuddedChest.Movable = false;
            AddItem(StuddedChest);

            Item StuddedArms = new StuddedArms();
            StuddedArms.Movable = false;
            AddItem(StuddedArms);

            Item StuddedLegs = new StuddedLegs();
            StuddedLegs.Movable = false;
            AddItem(StuddedLegs);

            Item StuddedGloves = new StuddedGloves();
            StuddedGloves.Movable = false;
            AddItem(StuddedGloves);

            Item StuddedGorget = new StuddedGorget();
            StuddedGorget.Movable = false;
            AddItem(StuddedGorget);

            switch (Utility.Random(7))
            {
                case 0: AddItem(new Pitchfork()); break;
                case 1: AddItem(new Spear()); break;
                case 2: AddItem(new Katana()); break;
                case 3: AddItem(new Axe()); break;
                case 4: AddItem(new Longsword()); break;
                case 5: AddItem(new Club()); break;
                case 6: AddItem(new QuarterStaff()); break;
            }

            Utility.AssignRandomHair(this);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
        }

        public override bool AlwaysMurderer { get { return true; } }

        public SanguinConscript(Serial serial) : base(serial)
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
