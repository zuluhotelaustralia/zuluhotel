using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a sanguin mender corpse")]
    public class SanguinMender : BaseCreature
    {
        [Constructable]
        public SanguinMender()
            : base(AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Sanguin Mender";
            Body = 0x190;
            Hue = Utility.RandomSkinHue();

            SetStr(80, 95);
            SetDex(35, 40);
            SetInt(450, 500);

            SetHits(200, 225);
            SetMana(450, 500);

            SetDamage(8, 14);

            VirtualArmor = 0;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.Magery, 75.0, 80.0);
            SetSkill(SkillName.EvalInt, 75.0, 80.0);
            SetSkill(SkillName.Meditation, 100.0, 100.0);

            SetSkill(SkillName.MagicResist, 75.0, 80.0);

            SetSkill(SkillName.Macing, 65.0, 70.0);
            SetSkill(SkillName.Wrestling, 65.0, 70.0);

            Fame = 2500;
            Karma = -2500;

            AddItem(new Sandals(Utility.RandomNeutralHue()));

            AddItem(new BlackStaff());
            
            Item Hood = new Hood();
            Hood.Movable = false;
            Hood.Hue = 1775;
            AddItem(Hood);

            Item Robe = new Robe();
            Robe.Movable = false;
            Robe.Hue = 1777;
            AddItem(Robe);
        }        

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.Rich, 2 );
	    AddLoot( LootPack.HighEarthScrolls );
	    AddLoot( LootPack.LowEarthScrolls );
	}

        public override bool CanRummageCorpses { get { return true; } }
        public override bool AlwaysMurderer { get { return true; } }
        public override int Meat { get { return 1; } }

        public SanguinMender(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
