using System;
using System.Collections.Generic;
using System.Text;

using Server.Items;

namespace Server.Mobiles
{
    public class Librarian : BaseCreature
    {
	[Constructable]
	public Librarian() : base( AIType.AI_Mage, FightMode.Weakest, 10, 1, 0.2, 0.4 )
	{
	    Name = "a librarian";
            Hue = 0x5E1;
            Body = 0x1D;
            BaseSoundID = 0x9E;

            //SpellCastAnimation = 10;
            //SpellCastFrameCount = 5; 

	    SetStr( 300, 305 );
	    SetDex( 70, 75 );
	    SetInt( 2500, 3000 );

	    SetHits( 700, 900 );
            SetMana( 2500, 3000);

	    SetDamage( 10, 18 );

            VirtualArmor = 10;

            SetSkill( SkillName.Tactics, 120.0, 130.0);

            SetSkill( SkillName.Magery, 70.0, 13.0);
            SetSkill( SkillName.Meditation, 120.0, 130.0);			
	    SetSkill( SkillName.EvalInt, 70.0, 150.0 );
		
	    SetSkill( SkillName.MagicResist, 70.0, 130.0 );

	    SetSkill( SkillName.Wrestling, 75.0, 130.0 );

	    Fame = 18000;
	    Karma = -18000;
	}

	public override OppositionGroup OppositionGroup
	{
	    get{ return OppositionGroup.FeyAndUndead; }
	}        

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.FilthyRich, 2 );
	    AddLoot( LootPack.Rich );
	    AddLoot( LootPack.LesserNecroScrolls, 2 );
	    AddLoot( LootPack.GreaterNecroScrolls, 4 );

	    if( Utility.RandomDouble() >= 0.9 ){
		PackItem( new ChicaneBossStone() );
	    }
	    if( Utility.RandomDouble() >= 0.99 ){
		PackItem( new NecromancerSpellbook() );
	    }
        } 

	public override bool CanRummageCorpses{ get{ return true; } }
	public override bool BleedImmune{ get{ return true; } }
	public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		

	public Librarian( Serial serial ) : base( serial )
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
