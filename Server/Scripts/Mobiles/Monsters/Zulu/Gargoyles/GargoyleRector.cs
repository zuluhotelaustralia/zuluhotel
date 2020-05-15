using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a gargoyle corpse")]
    public class GargoyleRector : BaseCreature
    {
        [Constructable]
        public GargoyleRector()
            : base(AIType.AI_Mage, FightMode.Weakest, 10, 1, 0.2, 0.4)
        {
            Name = "a gargoyle rector";
            Body = 130;
            Hue = 0;
            BaseSoundID = 372;

            SetStr(300, 320);
            SetDex(75, 80);
            SetInt(100, 120);

            SetHits(275, 300);
            SetMana(400, 500);

            SetDamage(14, 35);

            VirtualArmor = 25;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.EvalInt, 65.0, 70.0);
            SetSkill(SkillName.Magery, 65.0, 70.0);
            SetSkill(SkillName.Meditation, 100.0, 100.0);

            SetSkill(SkillName.MagicResist, 100.0, 110.0);

            SetSkill(SkillName.Wrestling, 85.0, 90.0);

            Fame = 3500;
            Karma = -3500;

        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
        }

        public override int Meat { get { return 1; } }

        public GargoyleRector(Serial serial)
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
