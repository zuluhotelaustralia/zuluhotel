using System;
using System.Collections.Generic;
using System.Text;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a decaying corpse")]
    public class FailedExperiment : BaseCreature
    {
        [Constructable]
	public FailedExperiment() : base( AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4 )
	{
	    Name = "a failed experiment";
	    Body = 3;
            Hue = 0x1CE;
	    BaseSoundID = 471;

	    SetStr( 46, 70 );
	    SetDex( 31, 50 );
	    SetInt( 26, 40 );

	    SetHits( 175, 200 );

	    SetDamage( 4, 8 );

            VirtualArmor = 5;

            SetSkill( SkillName.Tactics, 100.0, 100.0);

	    SetSkill( SkillName.MagicResist, 15.0, 20.0 );	
		
	    SetSkill( SkillName.Wrestling, 35.0, 40.0 );

	    Fame = 600;
	    Karma = -600;			
	}

	public override void GenerateLoot()
	{
	    AddLoot( LootPack.Rich );
	    
            PackItem(Loot.RandomBodyPart());
            PackItem(Loot.RandomBodyPart());
            PackItem(Loot.RandomBodyPart());
            PackItem(Loot.RandomBodyPart());
	}

	public override bool BleedImmune{ get{ return true; } }
	public override Poison PoisonImmune{ get{ return Poison.Regular; } }

	public FailedExperiment( Serial serial ) : base( serial )
	{
	}

	public override OppositionGroup OppositionGroup
	{
	    get{ return OppositionGroup.FeyAndUndead; }
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
