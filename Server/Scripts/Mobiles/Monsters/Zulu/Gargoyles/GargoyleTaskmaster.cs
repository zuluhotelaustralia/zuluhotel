using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a gargoyle corpse")]
    public class GargoyleTaskmaster : BaseCreature
    {
        [Constructable]
        public GargoyleTaskmaster()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.1, 0.4)
        {
            Name = "a gargoyle taskmaster"; // this guy runs fast, swings fast, and does decent damage. 
            Body = 753;
            Hue = 0;
            BaseSoundID = 372;

            SetStr(550, 650);
            SetDex(110, 140);
            SetInt(100, 120);

            SetHits(675, 750);
            SetMana(100, 120);

            SetDamage(25, 34);

            VirtualArmor = 25;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.EvalInt, 55.0, 60.0);
            SetSkill(SkillName.Magery, 55.0, 60.0);
            SetSkill(SkillName.Meditation, 100.0, 100.0);

            SetSkill(SkillName.MagicResist, 100.0, 110.0);

            SetSkill(SkillName.Wrestling, 75.0, 90.0);

            Fame = 3500;
            Karma = -3500;

        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
        }

        public override int Meat { get { return 1; } }

        public GargoyleTaskmaster(Serial serial)
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
