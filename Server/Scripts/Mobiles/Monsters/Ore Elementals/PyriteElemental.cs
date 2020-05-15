using System;
using Server;
using Server.Items;

//TOGENERATE
// wouldn't be too hard to do all this via the generator, there's basically one per ore type with various strengths but who gives a shit, just do them all the same and we can add air/fire/water/earth later
// maybe make the gems extra strong and a different loot group or smth

namespace Server.Mobiles
{
    [CorpseName("an ore elemental corpse")]
    public class PyriteElemental : BaseCreature
    {
        [Constructable]
        public PyriteElemental() : this(2)
        {
        }

        [Constructable]
        public PyriteElemental(int oreAmount) : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a pyrite elemental";
            Body = 107;
            BaseSoundID = 268;

            SetStr(226, 255);
            SetDex(126, 145);
            SetInt(71, 92);

            SetHits(136, 153);

            SetDamage(28);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 30, 40);
            SetResistance(ResistanceType.Fire, 40, 50);
            SetResistance(ResistanceType.Cold, 40, 50);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.MagicResist, 50.1, 95.0);
            SetSkill(SkillName.Tactics, 60.1, 100.0);
            SetSkill(SkillName.Wrestling, 60.1, 100.0);

            Fame = 3500;
            Karma = -3500;

            VirtualArmor = 32;

            Item ore = new PyriteOre(oreAmount);
            ore.ItemID = 0x19B9;
            PackItem(ore);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Gems, 2);
        }

        public override bool BleedImmune { get { return true; } }
        public override bool AutoDispel { get { return true; } }
        public override int TreasureMapLevel { get { return 1; } }

        public PyriteElemental(Serial serial) : base(serial)
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
