using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("a vaguely-humanoid corpse")]
    public class Headmaster1 : BaseCreature
    {
        [Constructable]
        public Headmaster1()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Name = "The Headmaster";
            Hue = Utility.RandomSkinHue();

            if (this.Female = Utility.RandomBool())
            {
                this.Body = 0x191; // female
            
            }
            else
            {
                this.Body = 0x190; // male
 
            }

            SetStr(50, 55);
            SetDex(60, 65);
            SetInt(235, 240);

            SetHits(525, 550);
	    SetMana(1000, 1000);
	    
            SetDamage(14, 20); //Uses Weapon

            VirtualArmor = 40;

            SetSkill(SkillName.Tactics, 100.0, 100.0);
	    SetSkill(SkillName.Magery, 150.0, 200.0);
            SetSkill(SkillName.Swords, 80.0, 85.0);
            SetSkill(SkillName.Macing, 80.0, 85.0);
            SetSkill(SkillName.Fencing, 80.0, 85.0);
            SetSkill(SkillName.Wrestling, 80.0, 85.0);
	    SetSkill(SkillName.EvalInt, 130.0, 130.0);
            SetSkill(SkillName.MagicResist, 130.0, 150.0);

            Fame = 5000;
            Karma = -5000;

            Utility.AssignRandomHair(this);

            Item Hat = new FloppyHat();
            Hat.Movable = false;
            Hat.Hue = 2759;
            AddItem(Hat);

            Item Surcoat = new Surcoat();
            Surcoat.Movable = false;
            Surcoat.Hue = 2759;
            AddItem(Surcoat);

            Item Cloak = new Cloak();
            Cloak.Movable = false;
            Cloak.Hue = 2759;
            AddItem(Cloak);

	    Item Pants = new LongPants();
	    Pants.Movable = false;
	    Pants.Hue = 2759;
	    AddItem(Pants);

	    Item Boots = new Boots();
	    Boots.Movable = false;
	    AddItem(Boots);
        }

        public override bool OnBeforeDeath()
        {
            Headmaster2 hm2 = new Headmaster2();
            hm2.Team = this.Team;
            hm2.Combatant = this.Combatant;
            hm2.NoKillAwards = true;
	    
	    Effects.PlaySound(this, Map, GetDeathSound());
            Effects.SendLocationEffect(Location, Map, 0x3709, 30, 10, 2759, 0);
            hm2.MoveToWorld(Location, Map);

            Delete();

            return false;
        }

        public override void GenerateLoot()
        {

        }

        public override bool AlwaysMurderer { get { return true; } }

        public Headmaster1(Serial serial)
            : base(serial)
        {
        }

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
    
    [CorpseName("a strange bear-like corpse")]
    public class Headmaster2 : BaseCreature
    {
        [Constructable]
        public Headmaster2()
            : base(AIType.AI_Melee, FightMode.Weakest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Name = "The Headmaster";
            Body = 213;
	    BaseSoundID = 0xA3; 

            SetStr(250, 255);
            SetDex(160, 165);
            SetInt(135, 140);

            SetHits(1225, 1250);

            SetDamage(35, 60);

            VirtualArmor = 40;

            SetSkill(SkillName.Tactics, 145.0, 150.0); //Uses Weapon

            SetSkill(SkillName.Swords, 180.0, 185.0);
            SetSkill(SkillName.Macing, 180.0, 185.0);
            SetSkill(SkillName.Fencing, 180.0, 185.0);
            SetSkill(SkillName.Wrestling, 180.0, 185.0);

            SetSkill(SkillName.MagicResist, 30.0, 50.0);

            Fame = 5000;
            Karma = -5000;
        }

        public override bool OnBeforeDeath()
        {
            Headmaster3 hm3 = new Headmaster3();
            hm3.Team = this.Team;
            hm3.Combatant = this.Combatant;
            hm3.NoKillAwards = true;
	    hm3.AddLoot( LootPack.SuperBoss );
	    hm3.AddLoot( LootPack.UltraRich, 2 );
	    hm3.AddLoot( LootPack.GreaterNecroScrolls );
	    hm3.AddLoot( LootPack.LesserNecroScrolls );
	    hm3.AddLoot( LootPack.Gems, 10 );
	    if( Utility.RandomDouble() > 0.98 ) {
		hm3.PackItem( new NecromancerSpellbook() );
	    }

	    Effects.PlaySound(this, Map, GetDeathSound());
            Effects.SendLocationEffect(Location, Map, 0x3709, 30, 10, 2759, 0);
            hm3.MoveToWorld(Location, Map);

            Delete();

	    EarthElementalLord ell = new EarthElementalLord();
	    ell.Team = this.Team;
	    ell.Name = "The Top Geology Student";
	    ell.MoveToWorld( new Point3D( 5160, 56, 20 ), Map.Felucca );

	    FireElementalLord fll = new FireElementalLord();
	    fll.Team = this.Team;
	    fll.Name = "The Top Chemistry Student";
	    fll.MoveToWorld( new Point3D( 5160, 39, 20 ), Map.Felucca );

	    WaterElementalLord wll = new WaterElementalLord();
	    wll.Team = this.Team;
	    wll.Name = "The Top Environmental Sciences Student";
	    wll.MoveToWorld( new Point3D( 5177, 56, 20 ), Map.Felucca );

	    AirElementalLord all = new AirElementalLord();
	    all.Team = this.Team;
	    all.Name = "The Top Philosophy Student";
	    all.MoveToWorld( new Point3D( 5177, 39, 20 ), Map.Felucca );
	    
            return false;
        }

        public override void GenerateLoot()
        {

        }

        public override bool AlwaysMurderer { get { return true; } }

        public Headmaster2(Serial serial)
            : base(serial)
        {
        }

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

    [CorpseName("the headmaster's corpse")]
    public class Headmaster3 : BaseCreature
    {
        [Constructable]
        public Headmaster3()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Name = "The Headmaster";
	    
	    Body = 22;
	    BaseSoundID = 377;

	    SetStr( 296, 325 );
	    SetDex( 286, 305 );
	    SetInt( 241, 365 );

	    SetHits( 2258, 2275 );

	    SetDamage( 5, 10 );

	    SetSkill( SkillName.EvalInt, 150.1, 165.0 );
	    SetSkill( SkillName.Magery, 150.1, 165.0 );
	    SetSkill( SkillName.MagicResist, 160.1, 175.0 );
	    SetSkill( SkillName.Tactics, 150.1, 170.0 );
	    SetSkill( SkillName.Wrestling, 150.1, 170.0 );

	    Fame = 25000;
	    Karma = -25000;

	    VirtualArmor = 60;

	}

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.UltraRich, 2 );
	    AddLoot( LootPack.FilthyRich );
	    AddLoot( LootPack.GreaterNecroScrolls );
	    AddLoot( LootPack.LesserNecroScrolls );
	    AddLoot( LootPack.Gems, 2 );

        }

        public override bool AlwaysMurderer { get { return true; } }
	public override bool Unprovokable { get { return true; } }
	
        public Headmaster3(Serial serial)
            : base(serial)
        {
        }

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
