using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a mage corpse")]
    public class BridgeMage : BaseCreature
    {
        [Constructable]
        public BridgeMage()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Bridge Mage";
            Body = 0x190;
            Hue = 1882;

            SetStr(80, 85);
            SetDex(35, 40);
            SetInt(500, 600);

            SetHits(255, 300);
            SetMana(500, 600);

            SetDamage(4, 8);

            VirtualArmor = 0;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.Meditation, 100.0, 100.0);
            SetSkill(SkillName.EvalInt, 75.0, 80.0);
            SetSkill(SkillName.Magery, 75.0, 80.0);

            SetSkill(SkillName.MagicResist, 75.0, 80.0);

            SetSkill(SkillName.Wrestling, 60.0, 65.0);

            Fame = 2500;
            Karma = -2500;

            AddItem(new Sandals(Utility.RandomNeutralHue()));

            Item Robe = new Robe();
            Robe.Movable = false;
            Robe.Hue = 2306;
            AddItem(Robe);

            Item HalfApron = new HalfApron();
            HalfApron.Movable = false;
            HalfApron.Hue = 1644;
            AddItem(HalfApron);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.HighScrolls, 2);
        }

        public override bool CanRummageCorpses { get { return true; } }
        public override bool AlwaysMurderer { get { return true; } }
        public override int Meat { get { return 1; } }

        public override OppositionGroup OppositionGroup
        {
            get { return OppositionGroup.SavagesAndOrcs; }
        }

        public BridgeMage(Serial serial)
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
