using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
    [CorpseName( "a water puddle corpse" )]
    public class Puddle : BaseCreature
    {
	[Constructable]
	public Puddle() : base( AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4 )
	{
	    Name = "a water puddle";
	    Body = 51;
	    BaseSoundID = 456;

	    Hue = 2119;

	    SetStr( 22, 34 );
	    SetDex( 16, 21 );
	    SetInt( 16, 20 );

	    SetHits( 25, 35 );

	    SetDamage( 2, 4 );

            VirtualArmor = 5;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

	    SetSkill( SkillName.MagicResist, 195.0, 200.0 );	
		
	    SetSkill( SkillName.Wrestling, 15.0, 20.0 );

	    Fame = 300;
	    Karma = -300;	
	}

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.Meager );
        }

	public Puddle( Serial serial ) : base( serial )
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
