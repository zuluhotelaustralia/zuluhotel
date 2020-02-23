using System;
using System.Collections.Generic;
using System.Text;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an alchemist corpse")]
    public class CastleAlchemist : BaseCreature
    {
        [Constructable]
        public CastleAlchemist()
            : base(AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("evil mage");
            Title = "the alchemist";

            //Title = Utility.Random(2) == 0 ? "the alchemist" : "the summoner";

            Body = Utility.RandomBool() ? 0x190 : 0x191;
            Hue = 1882;

            SetStr(100, 105);
            SetDex(35, 40);
            SetInt(500, 600);

            SetHits(150, 175);
            SetMana(500, 600);

            SetDamage(5, 10);

            VirtualArmor = 0;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.Meditation, 100.0, 100.0);
            SetSkill(SkillName.EvalInt, 80.0, 85.0);
            SetSkill(SkillName.Magery, 80.0, 85.0);

            SetSkill(SkillName.MagicResist, 80.0, 85.0);

            SetSkill(SkillName.Wrestling, 65.0, 70.0);

            Fame = 500;
            Karma = -500;

            AddItem(new Sandals(Utility.RandomNeutralHue()));

            Item Robe = new Robe();
            Robe.Movable = false;
            Robe.Hue = 2306;
            AddItem(Robe);

            
        }

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.Rich );
	    AddLoot( LootPack.Potions );
	}

        public override bool CanRummageCorpses { get { return true; } }
        public override bool AlwaysMurderer { get { return true; } }
        public override int Meat { get { return 1; } }

        public CastleAlchemist(Serial serial)
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
