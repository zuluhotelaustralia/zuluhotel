using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a gargoyle corpse")]
    public class GargoyleAugurer : BaseCreature
    {
        public override WeaponAbility GetWeaponAbility()
        {
            return WeaponAbility.WhirlwindAttack;
        }

        [Constructable]
        public GargoyleAugurer() : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a gargoyle augurer";
            Body = 0x2F2;
            Hue = 2013;
            BaseSoundID = 0x174;

            SetStr(400, 450);
            SetDex(60, 65);
            SetInt(800, 1000);

            SetHits(600, 700);
            SetMana(800, 1000);

            SetDamage(8, 16);

            VirtualArmor = 25;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.EvalInt, 85.0, 90.0);
            SetSkill(SkillName.Magery, 85.0, 90.0);
            SetSkill(SkillName.Meditation, 100.0, 100.0);

            SetSkill(SkillName.MagicResist, 85.0, 90.0);

            SetSkill(SkillName.Wrestling, 70.0, 75.0);

            Fame = 5000;
            Karma = -5000;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);
            AddLoot(LootPack.Rich);
        }

        public override int Meat { get { return 1; } }

        public GargoyleAugurer(Serial serial) : base(serial)
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
