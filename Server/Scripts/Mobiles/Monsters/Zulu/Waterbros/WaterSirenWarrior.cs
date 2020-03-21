using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a siren warrior corpse")]
    public class WaterSirenWarrior : BaseCreature
    {
        public override bool ShowFameTitle
        {
            get
            {
                return false;
            }
        }

        [Constructable]
        public WaterSirenWarrior()
            : base(AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a siren warrior";
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

            Item kryss = new Kryss();
            kryss.Movable = false;
            kryss.Hue = 1410;
            AddItem(kryss);

            Item woodenShield = new WoodenShield();
            woodenShield.Movable = false;
            woodenShield.Hue = 1410;
            AddItem(woodenShield);

            Utility.AssignRandomHair(this, 1410);

            SetStr(125, 130);
            SetDex(90, 95);
            SetInt(30, 35);

            SetHits(225, 250);

            SetDamage(12, 20);

            VirtualArmor = 0;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.MagicResist, 195.0, 200.0);

            SetSkill(SkillName.Parry, 45.0, 50.0); 

            SetSkill(SkillName.Fencing, 80.0, 85.0);
            SetSkill(SkillName.Wrestling, 80.0, 85.0); 

            Fame = 10500;
            Karma = -10500;
        }        

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.Rich, 2 );
        }

        public override bool CanRummageCorpses { get { return true; } }
        public override bool AlwaysMurderer { get { return true; } }
        public override int Meat { get { return 1; } }

        public WaterSirenWarrior(Serial serial)
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
