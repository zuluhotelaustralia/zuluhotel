using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "a dragon corpse" )]
    public class GoldenDragon : BaseCreature
    {
	[Constructable]
	public GoldenDragon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
	{
	    Name = "a golden dragon";
	    Body = 46;
	    Hue = 48;
	    BaseSoundID = 362;

	    SetStr( 1185, 1315 );
	    SetDex( 200, 300 );
	    SetInt( 650, 1050 );

	    SetHits( 1500, 2000 );

	    SetDamage( 35, 50 );

	    SetDamageType( ResistanceType.Physical, 75 );
	    SetDamageType( ResistanceType.Fire, 25 );

	    SetResistance( ResistanceType.Physical, 65, 75 );
	    SetResistance( ResistanceType.Fire, 80, 90 );
	    SetResistance( ResistanceType.Cold, 70, 80 );
	    SetResistance( ResistanceType.Poison, 60, 70 );
	    SetResistance( ResistanceType.Energy, 60, 70 );

	    SetSkill( SkillName.EvalInt, 100.0, 150.0 );
	    SetSkill( SkillName.Magery, 100.1, 150.0 );
	    SetSkill( SkillName.Meditation, 120.1, 150.0 );
	    SetSkill( SkillName.MagicResist, 100.5, 150.0 );
	    SetSkill( SkillName.Tactics, 97.6, 150.0 );
	    SetSkill( SkillName.Wrestling, 97.6, 150.0 );

	    Fame = 22500;
	    Karma = -22500;

	    VirtualArmor = 120;
	}

	public override void GenerateLoot()
	{
	    AddLoot( LootPack.UltraRich, 2 );
	    AddLoot( LootPack.FilthyRich, 3 );
	    AddLoot( LootPack.Gems, 5 );
	}

	public override int GetIdleSound()
	{
	    return 0x2D3;
	}

	public override int GetHurtSound()
	{
	    return 0x2D1;
	}

	public override bool ReacquireOnMovement{ get{ return true; } }
	public override bool HasBreath{ get{ return true; } } // fire breath enabled
	public override bool AutoDispel{ get{ return true; } }
	public override HideType HideType{ get{ return HideType.GoldenDragon; } }
	public override int Hides{ get{ return 40; } }
	public override int Meat{ get{ return 19; } }
	public override int Scales{ get{ return 12; } }
	public override ScaleType ScaleType{ get{ return (ScaleType)Utility.Random( 4 ); } }
	public override Poison PoisonImmune{ get{ return Poison.Greater; } }
	public override Poison HitPoison{ get{ return Utility.RandomBool() ? Poison.Greater : Poison.Lethal; } }
	public override int TreasureMapLevel{ get{ return 5; } }
	public override bool CanFly { get { return true; } }

	public GoldenDragon( Serial serial ) : base( serial )
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
