using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "a water elemental corpse" )]
    public class WaterElementalLord : BaseCreature
    {
	public override double DispelDifficulty{ get{ return 130.0; } }
	public override double DispelFocus{ get{ return 90.0; } }

	[Constructable]
	public WaterElementalLord () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
	{
	    Name = "a water elemental lord";
	    Body = 16;
	    BaseSoundID = 278;

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
	    ControlSlots = 3;
	    CanSwim = true;

	    PackItem( new BlackPearl( 3 ) );
	}

	public override void GenerateLoot()
	{
	    AddLoot( LootPack.Average );
	    AddLoot( LootPack.Rich );
	    AddLoot( LootPack.Gems );
	    AddLoot( LootPack.Potions );
	}

	public override bool BleedImmune{ get{ return true; } }
	public override int TreasureMapLevel{ get{ return 4; } }

	public WaterElementalLord( Serial serial ) : base( serial )
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
