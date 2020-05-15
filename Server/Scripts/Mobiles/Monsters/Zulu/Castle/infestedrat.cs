using System;
using Server.Mobiles;

namespace Server.Mobiles
{
    [CorpseName("an infested rat corpse")]
    public class InfestedRat : BaseCreature
    {
        [Constructable]
        public InfestedRat()
            : base(AIType.AI_Animal, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an infested rat";
            Body = 0xD7;
            BaseSoundID = 0x188;
            Hue = 1439;

            SetStr(80, 85);
            SetDex(50, 55);
            SetInt(20, 25);

            SetHits(100, 125);
            SetMana(0);

            SetDamage(5, 10);

            VirtualArmor = 10;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.MagicResist, 25.0, 30.0);

            SetSkill(SkillName.Wrestling, 60.0, 65.0);

            Fame = 2000;
            Karma = -2000;

            Tamable = true;
            ControlSlots = 2;
            MinTameSkill = 90.0;
        }

        public override Poison PoisonImmune { get { return Poison.Regular; } }
        public override Poison HitPoison { get { return Poison.Regular; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
        }

        public override int Meat { get { return 1; } }
        public override int Hides { get { return 6; } }
        public override FoodType FavoriteFood { get { return FoodType.Fish | FoodType.Meat | FoodType.FruitsAndVegies | FoodType.Eggs; } }

        public InfestedRat(Serial serial)
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
