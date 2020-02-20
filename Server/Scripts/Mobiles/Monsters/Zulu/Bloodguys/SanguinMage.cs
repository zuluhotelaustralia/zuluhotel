using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles 
{ 
    [CorpseName( "a sanguin mage corpse" )] 
    public class SanguinMage : BaseCreature 
    { 
	[Constructable] 
	public SanguinMage() : base( AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
	{ 
	    Name = "Sanguin Mage";
	    Body = 0x190;
	    Hue = Utility.RandomSkinHue(); 

	    SetStr( 80, 85 );
	    SetDex( 35, 40 );
	    SetInt( 500, 600 );

	    SetHits( 125, 150 );
            SetMana( 500, 600 );

	    SetDamage( 4, 8 );

            VirtualArmor = 0;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill (SkillName.Meditation, 100.0, 100.0);
	    SetSkill( SkillName.EvalInt, 75.0, 80.0 );
	    SetSkill( SkillName.Magery, 75.0, 80.0 );

	    SetSkill( SkillName.MagicResist, 75.0, 80.0 );

	    SetSkill( SkillName.Wrestling, 60.0, 65.0 );

	    Fame = 2500;
	    Karma = -2500;
			
	    AddItem( new Sandals( Utility.RandomNeutralHue() ) );
	    Item WizardsHat = new WizardsHat();
	    WizardsHat.Movable = false;
	    WizardsHat.Hue = 1777;
	    AddItem( WizardsHat );
	    Item Robe = new Robe();
	    Robe.Movable = false;
	    Robe.Hue = 1777;
	    AddItem( Robe );
	}       

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.FilthyRich );
	    AddLoot( LootPack.HighEarthScrolls, 2 );
	    AddLoot( LootPack.LowEarthScrolls, 2 );
	    AddLoot( LootPack.HighScrolls, 2 );
	    AddLoot( LootPack.EarthBookPack );
	}

	public override bool CanRummageCorpses{ get{ return true; } }
	public override bool AlwaysMurderer{ get{ return true; } }
	public override int Meat{ get{ return 1; } }

	public SanguinMage( Serial serial ) : base( serial )
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
