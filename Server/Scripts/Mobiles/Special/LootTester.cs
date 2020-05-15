using System;
using Server.Items;
using Server.Mobiles;
using Server.Spells;

namespace Server.Mobiles
{
    public class LootTester : BaseCreature
    {
        [Constructable]
        public LootTester() : base(AIType.AI_Melee, FightMode.Closest, 15, 1, 0.2, 0.6)
        {
            this.Body = 400;
            this.Hue = Utility.RandomSkinHue();
            this.CantWalk = true;
            this.Str = 250;
            this.Hits = 250;
            this.Name = "Bloggins the Loot Tester";

            Container pack = new Backpack();
            pack.Movable = false;
            AddItem(pack);

        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.SuperBoss);
            if (Utility.RandomDouble() >= 0.80)
            {
                PackItem(new SpellweavingBook());
                PackItem(new NecromancerSpellbook());
            }
        }

        public LootTester(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
