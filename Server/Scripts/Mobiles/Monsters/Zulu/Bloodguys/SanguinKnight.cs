using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("a sanguin defender corpse")]
    public class SanguinKnight : BaseCreature
    {
        [Constructable]
        public SanguinKnight() : base(AIType.AI_Melee, FightMode.Strongest, 10, 1, 0.1, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Name = "Sanguin Knight";
            Hue = Utility.RandomSkinHue();

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

            SetHits(225, 250);

            SetDamage(14, 20); //Uses Weapon

            VirtualArmor = 40;

            SetSkill(SkillName.Tactics, 100.0, 100.0); //Uses Weapon

            SetSkill(SkillName.Swords, 80.0, 85.0);
            SetSkill(SkillName.Macing, 80.0, 85.0);
            SetSkill(SkillName.Fencing, 80.0, 85.0);
            SetSkill(SkillName.Wrestling, 80.0, 85.0);

            SetSkill(SkillName.MagicResist, 30.0, 35.0);

            Fame = 3500;
            Karma = -3500;

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

            Item Hood = new Hood();
            Hood.Movable = false;
            Hood.Hue = 1775;
            AddItem(Hood);

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

            switch (Utility.Random(7))
            {
                case 0: AddItem(new VikingSword()); break;
                case 1: AddItem(new Halberd()); break;
                case 2: AddItem(new Bardiche()); break;
                case 3: AddItem(new LargeBattleAxe()); break;
                case 4: AddItem(new WarMace()); break;
                case 5: AddItem(new WarHammer()); break;
                case 6: AddItem(new WarFork()); break;
            }

            Utility.AssignRandomHair(this);
        }

        public override void GenerateLoot()
        {
            if (Utility.RandomDouble() >= 0.80)
            {
                Item Hood = new Hood();
                Hood.Hue = 1776;
                PackItem(Hood);
            }
            AddLoot(LootPack.FilthyRich, 2);
            AddLoot(LootPack.HighEarthScrolls);
            AddLoot(LootPack.LowEarthScrolls);
            AddLoot(LootPack.Rich);
        }

        public override bool AlwaysMurderer { get { return true; } }

        public SanguinKnight(Serial serial) : base(serial)
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
