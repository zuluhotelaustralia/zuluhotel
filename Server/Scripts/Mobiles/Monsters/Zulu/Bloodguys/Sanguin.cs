using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
    public class Sanguin : BaseCreature
    {
        [Constructable]
        public Sanguin() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Title = "the Sanguin conscript";
            Hue = Utility.RandomSkinHue();

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

            SetStr(86, 100);
            SetDex(151, 165);
            SetInt(161, 175);

            SetDamage(8, 10);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 35, 45);
            SetResistance(ResistanceType.Fire, 25, 30);
            SetResistance(ResistanceType.Cold, 25, 30);
            SetResistance(ResistanceType.Poison, 10, 20);
            SetResistance(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.Anatomy, 85.0);
            SetSkill(SkillName.Fencing, 85.0);
            SetSkill(SkillName.Macing, 85.0);
            SetSkill(SkillName.Poisoning, 60.0, 82.5);
            SetSkill(SkillName.MagicResist, 83.5, 92.5);
            SetSkill(SkillName.Swords, 85);
            SetSkill(SkillName.Tactics, 85);
            SetSkill(SkillName.Lumberjacking, 85);

            Fame = 1000;
            Karma = -1000;

            VirtualArmor = 40;


            Item Sandals = new Sandals();
            Sandals.Movable = false;
            Sandals.Hue = 1775;
            AddItem(Sandals);

            Item Surcoat = new Surcoat();
            Surcoat.Movable = false;
            Surcoat.Hue = 2106;
            AddItem(Surcoat);

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
                case 0: AddItem(new Longsword()); break;
                case 1: AddItem(new Cutlass()); break;
                case 2: AddItem(new Broadsword()); break;
                case 3: AddItem(new Axe()); break;
                case 4: AddItem(new Club()); break;
                case 5: AddItem(new Dagger()); break;
                case 6: AddItem(new Spear()); break;
            }
            Utility.AssignRandomHair(this);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Meager);
        }

        public override bool AlwaysMurderer { get { return true; } }

        public Sanguin(Serial serial) : base(serial)
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
