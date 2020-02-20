using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles 
{ 
    [CorpseName( "an evil mage corpse" )] 
    public class EvilMageRobed : BaseCreature 
    { 
	[Constructable] 
	public EvilMageRobed() : base( AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
	{ 
	    Name = NameList.RandomName( "evil mage" );
	    Title = "the evil mage";
	    Body = 0x190;
	    Hue = Utility.RandomSkinHue();

            SetStr(95, 100);
            SetDex(40, 45);
            SetInt(500, 600);

            SetHits(175, 200);
            SetMana(500, 600);

            SetDamage(4, 8);

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.EvalInt, 75.0, 120.0);
            SetSkill(SkillName.Magery, 75.0, 120.0);
            SetSkill(SkillName.Meditation, 100.0, 120.0);

            SetSkill(SkillName.MagicResist, 75.0, 120.0);

            SetSkill(SkillName.Wrestling, 60.0, 65.0);

	    Fame = 2500;
	    Karma = -2500;
			
	    AddItem( new Sandals( Utility.RandomNeutralHue() ) );

	    Item WizardsHat = new WizardsHat();
	    WizardsHat.Movable = false;
	    WizardsHat.Hue = Utility.RandomBlueHue();
	    AddItem( WizardsHat );

	    Item Robe = new Robe();
	    Robe.Movable = false;
	    Robe.Hue = Utility.RandomBlueHue();
	    AddItem( Robe );
	}		 

	public override void GenerateLoot()
	{
	    AddLoot( LootPack.Rich );
	    AddLoot( LootPack.HighScrolls, 3 );
	}

	public override bool CanRummageCorpses{ get{ return true; } }
	public override bool AlwaysMurderer{ get{ return true; } }
	public override int Meat{ get{ return 1; } }

	public EvilMageRobed( Serial serial ) : base( serial )
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
