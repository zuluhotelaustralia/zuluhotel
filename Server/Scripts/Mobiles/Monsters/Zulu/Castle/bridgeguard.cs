using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("a guard corpse")]
    public class BridgeGuard : BaseCreature
    {
        [Constructable]
        public BridgeGuard()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Name = "a Bridge Guard";
            Hue = 1882;

            if (this.Female = Utility.RandomBool())
            {
                this.Body = 0x191;
            }
            else
            {
                this.Body = 0x190;
            }

            SetStr(150, 155);
            SetDex(60, 65);
            SetInt(35, 40);

            SetHits(450, 500);

            SetDamage(14, 20); //Uses Weapon

            VirtualArmor = 40;

            SetSkill(SkillName.Tactics, 100.0, 100.0); //Uses Weapon

            SetSkill(SkillName.Swords, 80.0, 85.0);
            SetSkill(SkillName.Macing, 80.0, 85.0);
            SetSkill(SkillName.Fencing, 80.0, 85.0);
            SetSkill(SkillName.Wrestling, 80.0, 85.0);
            SetSkill(SkillName.Archery, 80.0, 85.0);

            SetSkill(SkillName.MagicResist, 30.0, 35.0);

            Fame = 3500;
            Karma = -3500;

            Int32 guardColor = 1755;


            Utility.AssignRandomHair(this, 2155);

            SetStr(100, 100);
            SetDex(80, 80);
            SetInt(10, 10);

            SetHits(500, 600);
            SetStam(80, 80);
            SetMana(10, 10);

            SetDamage(20, 40);

            VirtualArmor = 40;

            Item Cloak = new Cloak();
            Cloak.Movable = false;
            Cloak.Hue = guardColor;
            AddItem(Cloak);

            Item Surcoat = new Surcoat();
            Surcoat.Movable = false;
            Surcoat.Hue = guardColor;
            AddItem(Surcoat);

            Item PlateChest = new PlateChest();
            PlateChest.Movable = false;
            AddItem(PlateChest);

            Item PlateArms = new PlateArms();
            PlateArms.Movable = false;
            AddItem(PlateArms);

            Item PlateLegs = new PlateLegs();
            PlateLegs.Movable = false;
            AddItem(PlateLegs);

            Item PlateGloves = new PlateGloves();
            PlateGloves.Movable = false;
            AddItem(PlateGloves);

            Item PlateGorget = new PlateGorget();
            PlateGorget.Movable = false;
            AddItem(PlateGorget);



            switch (Utility.Random(3))
            {
                case 0:
                    {
                        AddItem(new Bow());
                    }
                    break;

                case 1:
                    {
                        AddItem(new VikingSword());
                    }
                    break;

                case 2:
                    {
                        AddItem(new Halberd());
                    }
                    break;
            }

        }

        public override void GenerateLoot()
        {
            AddLoot( LootPack.Rich );
        }

        public override bool AlwaysMurderer { get { return true; } }

        public override OppositionGroup OppositionGroup
        {
            get { return OppositionGroup.SavagesAndOrcs; }
        }

        public BridgeGuard(Serial serial)
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
