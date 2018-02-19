using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "an air elemental corpse" )]
    public class AirElementalLord : BaseCreature
    {
	public override double DispelDifficulty{ get{ return 130.0; } }
	public override double DispelFocus{ get{ return 90.0; } }

	// public BaseCreature(AIType ai,
	// 		    FightMode mode,
	// 		    int iRangePerception,
	// 		    int iRangeFight,
	// 		    double dActiveSpeed,
	// 		    double dPassiveSpeed)
	    
	[Constructable]
	public AirElementalLord () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
	{
	    Name = "an air elemental lord";
	    Body = 13;
	    Hue = 0x4001;
	    BaseSoundID = 655;

	    SetStr( 175, 225 );
	    SetDex( 175, 225 );
	    SetInt( 150, 175 );

	    SetDamage( 12, 16 );

	    SetSkill( SkillName.EvalInt, 90.0, 115.0 );
	    SetSkill( SkillName.Magery, 90.0, 115.0 );
	    SetSkill( SkillName.MagicResist, 90.0, 115.0 );
	    SetSkill( SkillName.Tactics, 90.0, 115.0 );
	    SetSkill( SkillName.Wrestling, 90.0, 115.0 );

	    Fame = 9000;
	    Karma = -4500;

	    VirtualArmor = 50;
	    ControlSlots = 2;
	}

	public override void GenerateLoot()
	{
	    AddLoot( LootPack.Average );
	    AddLoot( LootPack.Rich );
	    AddLoot( LootPack.Gems );
	    AddLoot( LootPack.MedScrolls );
	    AddLoot( LootPack.HighScrolls );
	}

	public override bool BleedImmune{ get{ return true; } }
	public override int TreasureMapLevel{ get{ return 4; } }

	public AirElementalLord( Serial serial ) : base( serial )
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

	    if ( BaseSoundID == 263 )
		BaseSoundID = 655;
	}
    }
}
