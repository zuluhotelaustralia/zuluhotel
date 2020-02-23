using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("an archer corpse")]
    public class CastleArcher : BaseCreature
    {
        [Constructable]
        public CastleArcher()
            : base(AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Name = "Castle Archer";
            Hue = 1882;

            if (this.Female = Utility.RandomBool())
            {
                this.Body = 0x191;
            }
            else
            {
                this.Body = 0x190;
            }

            SetStr(95, 100);
            SetDex(70, 80);
            SetInt(30, 35);

            SetHits(185, 220);

            SetDamage(8, 16); //Uses Weapon

            VirtualArmor = 15;

            SetSkill(SkillName.Tactics, 100.0, 100.0); //Uses Weapon

            SetSkill(SkillName.Archery, 75.0, 80.0);
            SetSkill(SkillName.Wrestling, 75.0, 80.0);

            SetSkill(SkillName.MagicResist, 30.0, 35.0);

            SetSkill(SkillName.Hiding, 95.0, 100.0);
            SetSkill(SkillName.Stealth, 95.0, 100.0);

            Fame = 1500;
            Karma = -1500;

            Item Sandals = new Sandals();
            Sandals.Movable = false;
            Sandals.Hue = 902;
            AddItem(Sandals);

            Item BodySash = new BodySash();
            BodySash.Movable = false;
            BodySash.Hue = 1755;
            AddItem(BodySash);

            Item Kilt = new Kilt();
            Kilt.Movable = false;
            Kilt.Hue = 1755;
            AddItem(Kilt);

            Item LeatherChest = new LeatherChest();
            LeatherChest.Movable = false;
            AddItem(LeatherChest);

            Item LeatherArms = new LeatherArms();
            LeatherArms.Movable = false;
            AddItem(LeatherArms);

            Item LeatherLegs = new LeatherLegs();
            LeatherLegs.Movable = false;
            AddItem(LeatherLegs);

            Item LeatherGloves = new LeatherGloves();
            LeatherGloves.Movable = false;
            AddItem(LeatherGloves);

            Item LeatherGorget = new LeatherGorget();
            LeatherGorget.Movable = false;
            LeatherGorget.Hue = 1755;
            AddItem(LeatherGorget);

            Item WideBrimHat = new WideBrimHat ();
            WideBrimHat.Movable = false;
            WideBrimHat.Hue = 1755;
            AddItem(WideBrimHat);

            switch (Utility.Random(2))
            {
                case 0: AddItem(new Bow()); break;
                case 1: AddItem(new Crossbow()); break;
            }

            Utility.AssignRandomHair(this);
        }

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.Rich );
	}

        public override bool AlwaysMurderer { get { return true; } }

        public CastleArcher(Serial serial)
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
