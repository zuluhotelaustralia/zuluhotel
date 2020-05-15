using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a gargoyle corpse")]
    public class GargoyleWarrior : BaseCreature
    {
        [Constructable]
        public GargoyleWarrior() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a gargoyle warrior";
            Body = 4;
            Hue = 2009;
            BaseSoundID = 372;

            SetStr(300, 305);
            SetDex(75, 80);
            SetInt(400, 500);

            SetHits(175, 200);
            SetMana(400, 500);

            SetDamage(14, 20);

            VirtualArmor = 25;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.EvalInt, 55.0, 60.0);
            SetSkill(SkillName.Magery, 55.0, 60.0);
            SetSkill(SkillName.Meditation, 100.0, 100.0);

            SetSkill(SkillName.MagicResist, 60.0, 65.0);

            SetSkill(SkillName.Wrestling, 65.0, 70.0);

            Fame = 3500;
            Karma = -3500;

            VirtualArmor = 25;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
        }

        public override int Meat { get { return 1; } }

        public GargoyleWarrior(Serial serial) : base(serial)
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
