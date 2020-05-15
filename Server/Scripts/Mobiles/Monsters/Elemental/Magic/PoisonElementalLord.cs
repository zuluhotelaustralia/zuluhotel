using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a poison elemental's corpse")]
    public class PoisonElementalLord : BaseCreature
    {
        [Constructable]
        public PoisonElementalLord() : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a poison elemental lord";
            Body = 16;
            BaseSoundID = 263;
            Hue = 1272;

            SetStr(526, 615);
            SetDex(266, 285);
            SetInt(561, 635);

            SetHits(456, 509);

            SetDamage(18, 22);

            SetSkill(SkillName.EvalInt, 100.1, 120.0);
            SetSkill(SkillName.Magery, 100.1, 120.0);
            SetSkill(SkillName.Meditation, 100.2, 120.0);
            SetSkill(SkillName.Poisoning, 100.1, 130.0);
            SetSkill(SkillName.MagicResist, 105.2, 125.0);
            SetSkill(SkillName.Tactics, 100.1, 120.0);
            SetSkill(SkillName.Wrestling, 1000.1, 120.0);

            Fame = 12500;
            Karma = -12500;

            VirtualArmor = 70;

            PackItem(new Nightshade(4));
            PackItem(new LesserPoisonPotion());
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich);
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Rich, 2);
            AddLoot(LootPack.HighScrolls);
        }

        public override bool BleedImmune { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }

        public override Poison HitPoison { get { return Poison.Lethal; } }
        public override double HitPoisonChance { get { return 1.0; } }

        public override int TreasureMapLevel { get { return 5; } }

        public PoisonElementalLord(Serial serial) : base(serial)
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
