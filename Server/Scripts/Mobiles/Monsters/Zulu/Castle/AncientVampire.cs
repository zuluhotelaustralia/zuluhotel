using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an ancient vampire's corpse")]
    public class AncientVampire : BaseCreature
    {
        [Constructable]
        public AncientVampire()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("ancient lich");
            Title = "the ancient vampire";
            Body = Utility.RandomBool() ? 0x190 : 0x191;
            BaseSoundID = 412;
            Hue = 1882;
            

            SetStr(400, 405);
            SetDex(95, 100);
            SetInt(4000, 5000);

            SetHits(1000, 1200);
            SetMana(4000, 5000);

            SetDamage(16, 24);

            VirtualArmor = 5;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.EvalInt, 120.0, 125.0);
            SetSkill(SkillName.Magery, 120.0, 125.0);
            SetSkill(SkillName.Meditation, 100.0, 100.0);

            SetSkill(SkillName.MagicResist, 120, 125);

            SetSkill(SkillName.Wrestling, 70.0, 75.0);

            Fame = 500;
            Karma = -500;

            

            Item Skirt = new Skirt();
            Skirt.Movable = false;
            Skirt.Hue = 2019;
            AddItem(Skirt);

            Item Cloak = new Cloak();
            Cloak.Movable = false;
            Cloak.Hue = 2019;
            AddItem(Cloak);
        }

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.FilthyRich, 2 );
	    AddLoot( LootPack.Rich );
	    AddLoot( LootPack.NecroBookPack );
	    AddLoot( LootPack.GreaterNecroScrolls );
	    AddLoot( LootPack.LesserNecroScrolls );
	    AddLoot( LootPack.Gems, 2 );
	    
            PackItem(new BonePile());

            if (0.99 > Utility.RandomDouble())
                PackItem(new Bone());

            if (0.03 > Utility.RandomDouble())
                PackItem(new CandleSkull());
        }

        public override bool AlwaysMurderer { get { return true; } }
        public override bool Unprovokable { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Deadly; } }

        public override OppositionGroup OppositionGroup
        {
            get { return OppositionGroup.FeyAndUndead; }
        }

        public override int GetIdleSound()
        {
            return 0x19D;
        }

        public override int GetAngerSound()
        {
            return 0x175;
        }

        public override int GetDeathSound()
        {
            return 0x108;
        }

        public override int GetAttackSound()
        {
            return 0xE2;
        }

        public override int GetHurtSound()
        {
            return 0x28B;
        }

        public AncientVampire(Serial serial)
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
