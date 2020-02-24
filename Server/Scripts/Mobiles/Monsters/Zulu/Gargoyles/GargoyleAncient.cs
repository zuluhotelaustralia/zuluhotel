using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an elder gargoyle corpse")]
    public class GargoyleAncient : BaseCreature
    {
        [Constructable]
        public GargoyleAncient()
            : base(AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an ancient gargoyle";
            Body = 722;
            Hue = 0;
            BaseSoundID = 372;

            SetStr(500, 505);
            SetDex(90, 100);
            SetInt(1000, 1250);

            SetHits(1400, 1700);
            SetMana(1000, 1250);

            SetDamage(35, 55);

            VirtualArmor = 20;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.EvalInt, 90.0, 105.0);
            SetSkill(SkillName.Meditation, 100.0, 100.0);
            SetSkill(SkillName.Magery, 90.0, 105.0);

            SetSkill(SkillName.MagicResist, 90.0, 95.0);

            SetSkill(SkillName.Wrestling, 90.0, 105.0);

            Fame = 24000;
            Karma = -24000;
        }

        public override void GenerateLoot()
        {
            PackItem(new Longsword());

	    AddLoot( LootPack.FilthyRich, 2 );
	    AddLoot( LootPack.Rich );
        }

        public override bool CanRummageCorpses { get { return true; } }

        public override int Meat { get { return 2; } }

        public GargoyleAncient(Serial serial)
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