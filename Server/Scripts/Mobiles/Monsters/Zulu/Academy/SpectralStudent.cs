using System;
using System.Collections.Generic;
using System.Text;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a ghostly corpse")]
    public class SpectralStudent : BaseCreature
    {
        [Constructable]
        public SpectralStudent() : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = Utility.Random(2) == 0 ? "a spectral apprentice" : "a spectral student";
            Body = 26;
            Hue = 0x4001;
            BaseSoundID = 0x482;

            SetStr(95, 100);
            SetDex(70, 75);
            SetInt(400, 450);

            SetHits(80, 100);
            SetMana(400, 450);

            SetDamage(3, 5);

            VirtualArmor = 5;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.Meditation, 100.0, 100.0);
            SetSkill(SkillName.EvalInt, 55.0, 60.0);
            SetSkill(SkillName.Magery, 55.0, 60.0);

            SetSkill(SkillName.MagicResist, 55.0, 60.0);

            SetSkill(SkillName.Wrestling, 90.0, 95.0);

            Fame = 4000;
            Karma = -4000;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.LesserNecroScrolls);
        }

        public override bool BleedImmune { get { return true; } }

        public override OppositionGroup OppositionGroup
        {
            get { return OppositionGroup.FeyAndUndead; }
        }

        public override Poison PoisonImmune { get { return Poison.Lethal; } }

        public SpectralStudent(Serial serial) : base(serial)
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
