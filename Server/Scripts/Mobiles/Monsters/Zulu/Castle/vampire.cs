using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a vampire corpse")]
    public class Vampire : BaseCreature
    {
        [Constructable]
        public Vampire()
            : base(AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a vampire";
            
            Body = Utility.RandomBool() ? 0x190 : 0x191;
            BaseSoundID = 412;
            Hue = 1882;

            SetStr(200, 205);
            SetDex(45, 50);
            SetInt(800, 1000);

            SetHits(500, 600);
            SetMana(1400, 1600);

            SetDamage(10, 20);

            VirtualArmor = 5;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.EvalInt, 95.0, 100.0);
            SetSkill(SkillName.Magery, 95.0, 100.0);
            SetSkill(SkillName.Meditation, 100.0, 100.0);

            SetSkill(SkillName.MagicResist, 95.0, 100.0);

            SetSkill(SkillName.Wrestling, 65.0, 70.0);

            Fame = 500;
            Karma = -500;

            Item ShortPants = new ShortPants();
            ShortPants.Movable = false;
            ShortPants.Hue = 2019;
            AddItem(ShortPants);

            Item BodySash = new BodySash();
            BodySash.Movable = false;
            BodySash.Hue = 2019;
            AddItem(BodySash);
        }

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.FilthyRich );

            PackItem(new Bone());

        }

        public override bool AlwaysMurderer { get { return true; } }
        public override bool Unprovokable { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Deadly; } }


        public override OppositionGroup OppositionGroup
        {
            get { return OppositionGroup.FeyAndUndead; }
        }

        public override bool CanRummageCorpses { get { return true; } }
        public override bool BleedImmune { get { return true; } }

        public Vampire(Serial serial)
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
