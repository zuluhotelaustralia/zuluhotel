using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a water nymph corpse")]
    public class WaterNymph : BaseCreature
    {
        public override bool ShowFameTitle
        {
            get
            {
                return false;
            }
        }

        [Constructable]
        public WaterNymph()
            : base(AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a water nymph";
            Hue = 2759;

            this.Female = true;

            Body = 0x191;

            Item leatherskirt = new LeatherSkirt();
            leatherskirt.Movable = false;
            leatherskirt.Hue = 1410;
            AddItem(leatherskirt);

            Item leatherbustier = new LeatherBustierArms();
            leatherbustier.Movable = false;
            leatherbustier.Hue = 1410;
            AddItem(leatherbustier);

            Item sandals = new Sandals();
            sandals.Movable = false;
            sandals.Hue = 1410;
            AddItem(sandals);

            Item spellbook = new Spellbook();
            spellbook.Movable = false;
            spellbook.Hue = 2219;
            AddItem(spellbook);

            Utility.AssignRandomHair(this, 2219);
           
            SetStr(100, 105);
            SetDex(80, 85);
            SetInt(500, 600);

            SetHits(100, 125);
            SetMana(500, 600);

            SetDamage(4, 8);

            VirtualArmor = 0;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.Meditation, 100.0, 100.0);
            SetSkill(SkillName.EvalInt, 75.0, 80.0);
            SetSkill(SkillName.Magery, 75.0, 80.0);

            SetSkill(SkillName.MagicResist, 195.0, 200.0);

            SetSkill(SkillName.Wrestling, 60.0, 65.0);

            Fame = 2500;
            Karma = -2500;
        }        

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.FilthyRich );
	    AddLoot( LootPack.PaganReagentsPack, 10 );
        }

        public override bool CanRummageCorpses { get { return true; } }
        public override bool AlwaysMurderer { get { return true; } }
        public override int Meat { get { return 1; } }

        public WaterNymph(Serial serial)
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
