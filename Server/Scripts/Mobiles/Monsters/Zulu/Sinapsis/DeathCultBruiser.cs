using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("a human corpse")]
    public class DeathCultBruiser : BaseCreature
    {
        [Constructable]
        public DeathCultBruiser() : base(AIType.AI_Melee, FightMode.Strongest, 10, 1, 0.1, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Name = NameList.RandomName("centaur") + " the death cultist";
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
            SetDex(160, 165);
            SetInt(35, 40);

            SetHits(325, 350);

            SetDamage(14, 20); //Uses Weapon

            VirtualArmor = 40;

            SetSkill(SkillName.Tactics, 100.0, 100.0); //Uses Weapon

            SetSkill(SkillName.Swords, 100.0, 185.0);
            SetSkill(SkillName.Macing, 100.0, 185.0);
            SetSkill(SkillName.Fencing, 100.0, 185.0);
            SetSkill(SkillName.Wrestling, 100.0, 185.0);

            SetSkill(SkillName.MagicResist, 30.0, 35.0);

            Fame = 3500;
            Karma = -3500;

            Item Sandals = new Sandals();
            Sandals.Movable = false;
            Sandals.Hue = 2746;
            AddItem(Sandals);

            Item Hood = new Hood();
            Hood.Movable = false;
            Hood.Hue = 2746;
            AddItem(Hood);

            Item Cloak = new Cloak();
            Cloak.Movable = false;
            Cloak.Hue = 2746;
            AddItem(Cloak);

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

        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);
            AddLoot(LootPack.GreaterNecroScrolls, 3);
            AddLoot(LootPack.Rich, 2);
        }

        public override bool AlwaysMurderer { get { return true; } }

        public DeathCultBruiser(Serial serial) : base(serial)
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
