using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
    [CorpseName( "an ophidian corpse" )]
    [TypeAlias( "Server.Mobiles.OphidianAvenger" )]
    public class OphidianDestroyer : BaseCreature
    {
	private static string[] m_Names = new string[]
	    {
		"an ophidian destroyer-errant",
		"an ophidian enrager"
	    };

	[Constructable]
	public OphidianDestroyer() : base( AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4 )
	{
	    Name = m_Names[Utility.Random( m_Names.Length )];
	    Body = 86;
	    BaseSoundID = 634;

	    SetStr( 350, 355 );
	    SetDex( 80, 85 );
	    SetInt( 35, 40 );

	    SetHits( 325, 350 );
	    SetMana( 0 );

	    SetDamage( 13, 26 );

            VirtualArmor = 15;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

	    SetSkill( SkillName.Poisoning, 100.0, 100.0 );
	    SetSkill( SkillName.MagicResist, 35.0, 40.0 );	
		
	    SetSkill( SkillName.Wrestling, 65.0, 70.0 );

	    Fame = 10000;
	    Karma = -10000;		
	}       

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.FilthyRich, 2 );
	    AddLoot( LootPack.Rich, 2 );
        }

	public override int Meat{ get{ return 2; } }

	public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
	public override Poison HitPoison{ get{ return Poison.Lethal; } }		

	public override OppositionGroup OppositionGroup
	{
	    get{ return OppositionGroup.TerathansAndOphidians; }
	}

	public OphidianDestroyer( Serial serial ) : base( serial )
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
