using System;
using System.Collections.Generic;
using System.Text;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a ghostly corpse")]
    public class RestlessStudent : BaseCreature
    {
        [Constructable]
	public RestlessStudent() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
	{
	    Name = Utility.Random(2) == 0 ? "a restless student" : "a restless apprentice";
	    Body = 148;
	    BaseSoundID = 451;

	    SetStr( 95, 100 );
	    SetDex( 40, 45 );
	    SetInt( 750, 1000 );

	    SetHits( 100, 125 );
            SetMana( 750, 1000);

	    SetDamage( 5, 10 );

            VirtualArmor = 15;

            SetSkill( SkillName.Tactics, 100.0, 100.0);

            SetSkill( SkillName.Meditation, 100.0, 100.0);
	    SetSkill( SkillName.EvalInt, 45.0, 50.0 );
	    SetSkill( SkillName.Magery, 45.0, 50.0 );

	    SetSkill( SkillName.MagicResist, 45.0, 50.0 );	
		
	    SetSkill( SkillName.Wrestling, 60.0, 65.0 );

	    Fame = 3000;
	    Karma = -3000;			
	}

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.Rich, 2 );
	    AddLoot( LootPack.LesserNecroScrolls );
        }
		
	public override bool BleedImmune{ get{ return true; } }

	public override OppositionGroup OppositionGroup
	{
	    get{ return OppositionGroup.FeyAndUndead; }
	}

	public override Poison PoisonImmune{ get{ return Poison.Regular; } }

	public RestlessStudent( Serial serial ) : base( serial )
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
