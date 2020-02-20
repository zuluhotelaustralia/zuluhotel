using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "an ophidian corpse" )]
    public class OphidianQueen : BaseCreature
    {
	[Constructable]
	public OphidianQueen() : base( AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4 )
	{
	    Name = "an ophidian matriarch";
	    Body = 87;
	    BaseSoundID = 644;

	    SetStr( 300, 305 );
	    SetDex( 50, 55 );
	    SetInt( 800, 1000 );

            SetHits( 600, 650 );
            SetMana( 800, 1000);

            SetDamage(12, 18);

            VirtualArmor = 15;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill( SkillName.EvalInt, 85.0, 90.0);
            SetSkill( SkillName.Magery, 85.0, 90.0);
            SetSkill(SkillName.Meditation, 100.0, 100.0);

            SetSkill( SkillName.MagicResist, 85.0, 90.0);

            SetSkill( SkillName.Wrestling, 75.0, 80.0);

	    Fame = 16000;
	    Karma = -16000;			
	}        

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.FilthyRich, 2 );
	    AddLoot( LootPack.Rich );
        } 

	public override Poison PoisonImmune{ get{ return Poison.Lethal; } }		

	public override OppositionGroup OppositionGroup
	{
	    get{ return OppositionGroup.TerathansAndOphidians; }
	}

	public OphidianQueen( Serial serial ) : base( serial )
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
