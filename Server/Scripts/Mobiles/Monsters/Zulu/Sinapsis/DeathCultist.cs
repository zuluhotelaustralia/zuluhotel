using System; 
using Server;
using Server.Items;

namespace Server.Mobiles 
{ 
    [CorpseName( "a human corpse" )] 
    public class DeathCultist : BaseCreature 
    { 
	[Constructable] 
	public DeathCultist() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
	{ 
	    Name = NameList.RandomName( "centaur" ) + " the death cultist";
	    Hue = Utility.RandomSkinHue();

	    if ( this.Female = Utility.RandomBool() ) 
	    { 
		this.Body = 0x191; 
	    } 
	    else 
	    { 
		this.Body = 0x190; 
	    }

	    SetStr( 95, 100 );
	    SetDex( 40, 45 );
	    SetInt( 500, 600 );

	    SetHits( 250, 275 );
            SetMana( 500, 600);

	    SetDamage( 4, 8 );

            SetSkill(SkillName.Tactics, 100.0, 100.0);

	    SetSkill( SkillName.EvalInt, 85.0, 140.0 );
	    SetSkill( SkillName.Magery, 85.0, 140.0 );
	    SetSkill( SkillName.Meditation, 100.0, 140.0 );

	    SetSkill( SkillName.MagicResist, 85.0, 140.0 );			

	    SetSkill( SkillName.Wrestling, 70.0, 75.0 );

	    Fame = 3500;
	    Karma = -3500;

	    VirtualArmor = 0;

	    AddItem( new Sandals( Utility.RandomNeutralHue() ) );

	    Item Robe = new Robe();
	    Robe.Movable = false;
	    Robe.Hue = 2746; //VERY Black(tm)
	    AddItem( Robe );
	}        

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.FilthyRich, 2 );
	    AddLoot( LootPack.LesserNecroScrolls, 3 );
	    AddLoot( LootPack.GreaterNecroScrolls, 2 );
	}

	public override bool AlwaysMurderer{ get{ return true; } }
	public override int Meat{ get{ return 1; } }		

	public DeathCultist( Serial serial ) : base( serial ) 
	{ 
	} 

	public override void Serialize( GenericWriter writer ) 
	{ 
	    base.Serialize( writer ); 
	    writer.Write( (int) 0 ); 
	} 

	public override void Deserialize( GenericReader reader ) 
	{ 
	    base.Deserialize( reader ); 
	    int version = reader.ReadInt(); 
	} 
    } 
}
