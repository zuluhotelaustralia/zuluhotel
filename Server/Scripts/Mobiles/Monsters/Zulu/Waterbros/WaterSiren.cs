using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an siren corpse")]
    public class WaterSiren : BaseCreature
    {
        public override bool ShowFameTitle
        {
            get
            {
                return false;
            }
        }

        [Constructable]
        public WaterSiren()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a siren";
            Hue = 2224;

            this.Female = true;

            Body = 0x191;

            Item leatherskirt = new LeatherSkirt();
            leatherskirt.Movable = false;
            leatherskirt.Hue = 2219;
            AddItem(leatherskirt);

            Item leatherbustier = new LeatherBustierArms();
            leatherbustier.Movable = false;
            leatherbustier.Hue = 2219;
            AddItem(leatherbustier);

            Item sandals = new Sandals();
            sandals.Movable = false;
            sandals.Hue = 2219;
            AddItem(sandals);

            Item spellbook = new Spellbook();
            spellbook.Movable = false;
            spellbook.Hue = 2219;
            AddItem(spellbook);

            Item lantern = new Lantern();
            lantern.Movable = false;
            lantern.Hue = 2219;            
            AddItem(lantern);           

            Utility.AssignRandomHair(this, 1410);

            SetStr(100, 105);
            SetDex(65, 70);
            SetInt(500, 600);

            SetHits(175, 200);
            SetMana(500, 600);

            SetDamage(5, 10);

            VirtualArmor = 0;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.Meditation, 100.0, 100.0);
            SetSkill(SkillName.EvalInt, 80.0, 85.0);
            SetSkill(SkillName.Magery, 80.0, 85.0);

            SetSkill(SkillName.MagicResist, 195.0, 200.0);

            SetSkill(SkillName.Wrestling, 70.0, 75.0);

            Fame = 10500;
            Karma = -10500;
        }        

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.FilthyRich );
	    AddLoot( LootPack.PaganReagentsPack, 20 );
        }

        public override bool CanRummageCorpses { get { return true; } }
        public override bool AlwaysMurderer { get { return true; } }
        public override int Meat { get { return 1; } }

        public WaterSiren(Serial serial)
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
