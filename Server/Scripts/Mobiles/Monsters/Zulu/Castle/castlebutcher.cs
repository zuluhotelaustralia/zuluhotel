using System;
using System.Collections.Generic;
using System.Text;
using Server.Items;

namespace Server.Mobiles
{
    public class CastleButcher : BaseCreature
    {
        [Constructable]
        public CastleButcher()
            : base(AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            int random = Utility.Random(2);

            SpeechHue = Utility.RandomDyedHue();
            Title = "the butcher";

            //Title = random == 0 ? "the butcher" : "the servant"; 

            Hue = 1882;

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

            SetStr(250, 300);
            SetDex(70, 75);
            SetInt(35, 40);

            SetHits(500, 600);

            SetDamage(14, 20);

            VirtualArmor = 0;

            SetSkill(SkillName.Tactics, 100.0, 100.0); //Uses Weapon

            SetSkill(SkillName.MagicResist, 35.0, 40.0);

            SetSkill(SkillName.Swords, 85.0, 90.0);

            Fame = 5000;
            Karma = -5000;


            AddItem(new ThighBoots(Utility.RandomRedHue()));

            Item FullApron = new FullApron();
            FullApron.Movable = false;
            FullApron.Hue = 1644;
            AddItem(FullApron);

            Item FancyShirt = new FancyShirt();
            FancyShirt.Movable = false;
            FancyShirt.Hue = 2306;
            AddItem(FancyShirt);

            Item ShortPants = new ShortPants();
            ShortPants.Movable = false;
            ShortPants.Hue = 2306;
            AddItem(ShortPants);

            

            switch (Utility.Random(4))
            {
                case 0: AddItem(new ExecutionersAxe()); break;
                case 1: AddItem(new Axe()); break;
                case 2: AddItem(new LargeBattleAxe()); break;
                case 3: AddItem(new DoubleAxe()); break;
            }

            Utility.AssignRandomHair(this);
        }

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.FilthyRich );
	    
            PackItem(new RawRibs(5));

            PackItem(Loot.RandomBodyPart());
            PackItem(Loot.RandomBodyPart());
            PackItem(Loot.RandomBodyPart());
        }

        public override bool AlwaysMurderer { get { return true; } }

        public CastleButcher(Serial serial)
            : base(serial)
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
