using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("a sanguin assassin corpse")]
    public class SanguinAssassin : BaseCreature
    {
        [Constructable]
        public SanguinAssassin()
            : base(AIType.AI_Melee, FightMode.Weakest, 10, 1, 0.1, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Name = "Sanguin Assassin";
            Hue = Utility.RandomSkinHue();

            if (this.Female = Utility.RandomBool())
            {
                this.Body = 0x191;
            }
            else
            {
                this.Body = 0x190;
            }

            SetStr(96, 120);
            SetDex(80, 90);
            SetInt(36, 60);

            SetHits(150, 175);
            SetStam(80, 90);

            SetDamage(8, 16); //Uses Weapon

            VirtualArmor = 10;

            SetSkill(SkillName.Tactics, 100.0, 100.0); //Uses Weapon

            SetSkill(SkillName.Fencing, 75.0, 80.0);
            SetSkill(SkillName.Wrestling, 75.0, 80.0);

            SetSkill(SkillName.MagicResist, 30.0, 35.0);

            SetSkill(SkillName.Hiding, 95.0, 100.0);
            SetSkill(SkillName.Stealth, 95.0, 100.0);

            SetSkill(SkillName.Poisoning, 60.0, 65.0);

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

            Item Cloak = new Cloak();
            Cloak.Movable = false;
            Cloak.Hue = 2106;
            AddItem(Cloak);

            Item LeatherArms = new LeatherArms();
            LeatherArms.Movable = false;
            AddItem(LeatherArms);

            Item LeatherGloves = new LeatherGloves();
            LeatherGloves.Movable = false;
            AddItem(LeatherGloves);

            Item LeatherGorget = new LeatherGorget();
            LeatherGorget.Movable = false;
            AddItem(LeatherGorget);

            Item Hood = new Hood();
            Hood.Movable = false;
            Hood.Hue = 1775;
            AddItem(Hood);

            switch (Utility.Random(3))
            {
                case 0: AddItem(new Dagger()); break;
                case 1: AddItem(new Kryss()); break;
                case 2: AddItem(new WarFork()); break;
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
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.HighEarthScrolls);
        }

        public override bool AlwaysMurderer { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Deadly; } }
        public override Poison HitPoison { get { return Poison.Deadly; } }

        public SanguinAssassin(Serial serial)
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
