using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles 
{ 
    [CorpseName( "a sanguin wizard corpse" )] 
    public class SanguinWizard : BaseCreature 
    { 
	[Constructable] 
	public SanguinWizard() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
	{ 
	    Name = "Sanguin Wizard";
	    Body = 0x190;
	    Hue = Utility.RandomSkinHue(); 

	    SetStr( 80, 85 );
	    SetDex( 35, 40 );
	    SetInt( 800, 1000 );

            SetHits(200, 225);
            SetMana(800, 1000);

            SetDamage(4, 8);

            VirtualArmor = 0;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.Meditation, 100.0, 100.0);
            SetSkill(SkillName.EvalInt, 85.0, 120.0);
            SetSkill(SkillName.Magery, 85.0, 90.0);

            SetSkill(SkillName.MagicResist, 75.0, 80.0);

            SetSkill(SkillName.Wrestling, 70.0, 75.0);

	    Fame = 2500;
	    Karma = -2500;
			
	    AddItem( new Sandals( Utility.RandomNeutralHue() ) );

	    Item Hood = new Hood();
	    Hood.Movable = false;
	    Hood.Hue = 1776;
	    AddItem( Hood );

	    Item Robe = new Robe();
	    Robe.Movable = false;
	    Robe.Hue = 1776;
	    AddItem( Robe );
	}        

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.FilthyRich, 2 );
	    AddLoot( LootPack.HighEarthScrolls, 2 );
	    AddLoot( LootPack.LowEarthScrolls, 2 );

	    if( Utility.RandomDouble() >= 0.80 ){
		Item Hood = new Hood();
		Hood.Hue = 1776;
		PackItem( Hood );
	    }
	    if( Utility.RandomDouble() >= 0.95 ){
		PackItem( new SpellweavingBook() );
	    }
        }

	public override bool CanRummageCorpses{ get{ return true; } }
	public override bool AlwaysMurderer{ get{ return true; } }
	public override int Meat{ get{ return 1; } }

	public SanguinWizard( Serial serial ) : base( serial )
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
