using System; 
using System.Collections; 
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network; 

namespace Server.Mobiles 
{ 	
    [CorpseName( "a sanguin defender corpse" )] 
    public class SanguinDefender : BaseCreature 
    { 
	[Constructable] 
	public SanguinDefender() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
	{ 
	    SpeechHue = Utility.RandomDyedHue(); 
	    Name = "Sanguin Defender"; 
	    Hue = Utility.RandomSkinHue(); 

	    if ( this.Female = Utility.RandomBool() ) 
	    { 
		this.Body = 0x191; 
	    } 
	    else 
	    { 
		this.Body = 0x190; 
	    } 

	    SetStr( 125, 130 );
	    SetDex( 50, 55 );
	    SetInt( 30, 35 );

	    SetHits( 150, 175 );

	    SetDamage( 10, 16 ); //Uses Weapon

            VirtualArmor = 25;                      

            SetSkill(SkillName.Tactics, 100.0, 100.0); //Uses Weapon

            SetSkill(SkillName.Swords, 70.0, 75.0);
            SetSkill(SkillName.Macing, 70.0, 75.0);
            SetSkill(SkillName.Fencing, 70.0, 75.0);
            SetSkill(SkillName.Wrestling, 70.0, 75.0);			

            SetSkill(SkillName.Parry, 50.0, 55.0);
           
	    SetSkill( SkillName.MagicResist, 30.0, 35.0 );

	    Fame = 2500;
	    Karma = -2500;			
			
	    Item Sandals = new Sandals();
	    Sandals.Movable = false;
	    Sandals.Hue = 1775;
	    AddItem( Sandals );
			
	    Item Surcoat = new Surcoat();
	    Surcoat.Movable = false;
	    Surcoat.Hue = 2106;
	    AddItem( Surcoat );		

	    Item Kilt = new Kilt();
	    Kilt.Movable = false;
	    Kilt.Hue = 1775;
	    AddItem( Kilt );		

	    Item Hood = new Hood();
	    Hood.Movable = false;
	    Hood.Hue = 1775;
	    AddItem( Hood );					
			
	    Item StuddedChest = new StuddedChest();
	    StuddedChest.Movable = false;
	    AddItem( StuddedChest );

	    Item StuddedArms = new StuddedArms();
	    StuddedArms.Movable = false;
	    AddItem( StuddedArms );

	    Item StuddedLegs = new StuddedLegs();
	    StuddedLegs.Movable = false;
	    AddItem( StuddedLegs );

	    Item StuddedGloves = new StuddedGloves();
	    StuddedGloves.Movable = false;
	    AddItem( StuddedGloves );

	    Item StuddedGorget = new StuddedGorget();
	    StuddedGorget.Movable = false;
	    AddItem( StuddedGorget );

	    Item MetalKiteShield = new MetalKiteShield();
	    MetalKiteShield.Movable = false;
	    AddItem(MetalKiteShield);
			
	    switch ( Utility.Random( 6 ))
	    {
		case 0: AddItem( new Broadsword() ); break;
		case 1: AddItem( new VikingSword() ); break;
		case 2: AddItem( new Mace() ); break;				
		case 3: AddItem( new Maul() ); break;
		case 4: AddItem( new Kryss() ); break;
		case 5: AddItem( new WarFork() ); break;               
	    }

	    Utility.AssignRandomHair( this );
	}        

        public override void GenerateLoot()
        {
	    if( Utility.RandomDouble() >= 0.80 ){
		Item Hood = new Hood();
		Hood.Hue = 1776;
		PackItem( Hood );
	    }
	    AddLoot( LootPack.FilthyRich );
	    AddLoot( LootPack.HighEarthScrolls, 2 );
	    AddLoot( LootPack.Potions );
	}

	public override bool AlwaysMurderer{ get{ return true; } }

	public SanguinDefender( Serial serial ) : base( serial ) 
	{ 
	} 

	public override void Serialize( GenericWriter writer ) 
	{ 
	    base.Serialize( writer ); 

	    writer.Write( (int) 0 ); // version 
	} 

	public override void Deserialize( GenericReader reader ) 
	{ 
	    base.Deserialize( reader ); 

	    int version = reader.ReadInt(); 
	} 
    } 
}
