using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{

    [CorpseName("a crazed adventurer's corpse")]
    public class CrazedAdventurer : BaseCreature
    {
        public override bool ShowFameTitle
        {
            get
            {
                return false;
            }
        }

        [Constructable]
        public CrazedAdventurer()
            : base(AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Name = "a crazed adventurer";
            Hue = 2155;

            if (this.Female = Utility.RandomBool())
            {
                this.Body = 0x191;
            }
            else
            {
                this.Body = 0x190;
            }

            SetStr(150, 155);
            SetDex(70, 75);
            SetInt(35, 40);

            SetHits(200, 225);
            SetStam(70, 75);

            SetDamage(10, 16); //Uses Weapon

            VirtualArmor = 10;

            SetSkill(SkillName.Tactics, 100.0, 100.0); //Uses Weapon

            SetSkill(SkillName.MagicResist, 30.0, 35.0);

            SetSkill(SkillName.Parry, 35.0, 40.0);

            SetSkill(SkillName.Swords, 75.0, 80.0);
            SetSkill(SkillName.Macing, 75.0, 80.0);
            SetSkill(SkillName.Fencing, 75.0, 80.0);
            SetSkill(SkillName.Archery, 75.0, 80.0);
            SetSkill(SkillName.Wrestling, 75.0, 80.0);            

            Fame = 1500;
            Karma = -1500;

            Item boots = new Boots();
            boots.Movable = false;
            boots.Hue = 2155;
            AddItem(boots);

            Item Cloak = new Cloak();
            Cloak.Movable = false;
            Cloak.Hue = 2155;
            AddItem(Cloak);

            Item leatherchest = new LeatherChest();
            leatherchest.Movable = false;
            AddItem(leatherchest);

            Item LeatherGloves = new LeatherGloves();
            LeatherGloves.Movable = false;
            AddItem(LeatherGloves);

            Item LeatherGorget = new LeatherGorget();
            LeatherGorget.Movable = false;
            AddItem(LeatherGorget);

            Item leatherlegs = new LeatherLegs();
            leatherlegs.Movable = false;
            AddItem(leatherlegs);

            switch (Utility.Random(4))
            {
                case 0:
                    {
                        AddItem(new Crossbow());
                        PackItem(new CrossbowBolts(50));
                    }
		    break;

                case 1:
                    {
                        AddItem(new Broadsword());
                        AddItem(new WoodenShield());
                    }
		    break;

                case 2:
                    {
                        AddItem(new Mace());
                        AddItem(new WoodenShield());
                    }
		    break;

                case 3:
                    {
                        AddItem(new ShortSpear());
                    }
		    break;
            }

            Utility.AssignRandomHair(this, 2155);
        }        

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.Rich, 2 );
	    
            PackItem(new Bedroll());
            PackItem(new CookedBird());
            PackItem(new Torch());
        }

        public override bool AlwaysMurderer { get { return true; } }

        public CrazedAdventurer(Serial serial)
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
