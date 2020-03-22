using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles 
{ 
    [CorpseName( "a sanguin healer corpse" )] 
    public class SanguinHealer : BaseCreature 
    { 
	[Constructable] 
	public SanguinHealer() : base( AIType.AI_Healer, FightMode.Weakest, 10, 1, 0.1, 0.4 ) 
	{ 
	    Name = "Sanguin Healer";
	    Body = 0x190;
	    Hue = Utility.RandomSkinHue(); 

	    SetStr( 80, 85 );
	    SetDex( 35, 40 );
	    SetInt( 400, 450 );

            SetHits( 150, 175);
            SetMana( 400, 450);

            SetDamage(6, 10);

            VirtualArmor = 0;

            SetSkill(SkillName.Tactics, 100.0, 100.0);                       

            SetSkill(SkillName.Magery, 70.0, 75.0);
            SetSkill(SkillName.EvalInt, 70.0, 75.0);
            SetSkill(SkillName.Meditation, 100.0, 100.0);

            SetSkill(SkillName.MagicResist, 70.0, 75.0); 

            SetSkill(SkillName.Wrestling, 60.0, 65.0);

            Fame = 2500;
            Karma = -2500;

	    AddItem( new Sandals( Utility.RandomNeutralHue() ) );

            AddItem(new QuarterStaff());

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
	    AddLoot( LootPack.Rich, 2 );
	    AddLoot( LootPack.LowEarthScrolls, 2 );
	    AddLoot( LootPack.HighEarthScrolls );
	}
		 
	public override bool CanRummageCorpses{ get{ return true; } }
	public override bool AlwaysMurderer{ get{ return true; } }
	public override int Meat{ get{ return 1; } }

	public SanguinHealer( Serial serial ) : base( serial )
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
