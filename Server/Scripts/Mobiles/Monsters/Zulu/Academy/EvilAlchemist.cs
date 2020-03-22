using System;
using System.Collections.Generic;
using System.Text;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a magical corpse")]
    public class EvilAlchemist : BaseCreature
    {
	[Constructable] 
	public EvilAlchemist() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
	{ 
	    Name = NameList.RandomName( "evil mage" );
            Title = "the alchemist";

	    //Title = Utility.Random(2) == 0 ? "the alchemist" : "the summoner";

	    Body = Utility.RandomBool()?0x190:0x191;
            Hue = Utility.RandomSkinHue();

	    SetStr( 100, 105 );
	    SetDex( 35, 40 );
	    SetInt( 500, 600 );

	    SetHits( 150, 175 );
            SetMana( 500, 600);

	    SetDamage( 5, 10 );

            VirtualArmor = 0;

            SetSkill( SkillName.Tactics, 100.0, 100.0);

            SetSkill( SkillName.Meditation, 100.0, 100.0);
	    SetSkill( SkillName.EvalInt, 80.0, 85.0 );
            SetSkill( SkillName.Magery, 80.0, 85.0);	

            SetSkill( SkillName.MagicResist, 80.0, 85.0);

	    SetSkill( SkillName.Wrestling, 65.0, 70.0 );

	    Fame = 2500;
	    Karma = -2500;

	    AddItem( new Robe( Utility.RandomNeutralHue() ) ); // TODO: Proper hue
	    AddItem( new Sandals() );
	}

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.FilthyRich );
	    AddLoot( LootPack.LesserNecroScrolls );
	}

	public override bool CanRummageCorpses{ get{ return true; } }
	public override bool AlwaysMurderer{ get{ return true; } }
	public override int Meat{ get{ return 1; } }		

	public EvilAlchemist( Serial serial ) : base( serial )
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
