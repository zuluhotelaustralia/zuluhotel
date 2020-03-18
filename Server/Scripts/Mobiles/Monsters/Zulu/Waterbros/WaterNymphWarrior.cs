using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a water nymph warrior corpse")]
    public class WaterNymphWarrior : BaseCreature
    {
        public override bool ShowFameTitle
        {
            get
            {
                return false;
            }
        }

        [Constructable]
        public WaterNymphWarrior()
            : base(AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a water nymph warrior";
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
            
            Item qstaff = new QuarterStaff();
            qstaff.Movable = false;
            qstaff.Hue = 1410;
            AddItem(qstaff);

            Utility.AssignRandomHair(this, 2219);

            SetStr(125, 130);
            SetDex(60, 65);
            SetInt(30, 35);

            SetHits(200, 225);

            SetDamage(10, 16);

            VirtualArmor = 0;

            SetSkill(SkillName.Tactics, 100.0, 100.0); 
            
            SetSkill(SkillName.MagicResist, 195.0, 200.0);

            SetSkill(SkillName.Macing, 70.0, 75.0);

            Fame = 2500;
            Karma = -2500;
        }        

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.Rich, 2 );
	    AddLoot( LootPack.LowEarthScrolls );
        }

        public override bool CanRummageCorpses { get { return true; } }
        public override bool AlwaysMurderer { get { return true; } }
        public override int Meat { get { return 1; } }

        public WaterNymphWarrior(Serial serial)
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
