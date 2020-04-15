using System;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
    [FlipableAttribute( 0x13FD, 0x13FC )]
    public class HeavyCrossbow : BaseRanged
    {
	public override int EffectID{ get{ return 0x1BFE; } }
	public override Type AmmoType{ get{ return typeof( Bolt ); } }
	public override Item Ammo{ get{ return new Bolt(); } }

	public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.MovingShot; } }
	public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Dismount; } }

	public override int AosStrengthReq{ get{ return 80; } }
	public override int AosMinDamage{ get{ return Core.ML ? 20 : 19; } }
	public override int AosMaxDamage{ get{ return Core.ML ? 24 : 20; } }
	public override int AosSpeed{ get{ return 22; } }
	public override float MlSpeed{ get{ return 5.00f; } }

	public override int OldStrengthReq{ get{ return 40; } }
	public override int OldMinDamage{ get{ return 21; } }
	public override int OldMaxDamage{ get{ return 56; } }
	public override int OldSpeed{ get{ return 20; } }

	public override int DefMaxRange{ get{ return 8; } }

	public override int InitMinHits{ get{ return 31; } }
	public override int InitMaxHits{ get{ return 100; } }

	public override double GetBaseDamage( Mobile attacker ){
	    if( attacker is BaseCreature ){
		return base.GetBaseDamage( attacker );
	    }
	    
	    int damage = Utility.Dice( 5, 10, 6 );

	    if ( DamageLevel != WeaponDamageLevel.Regular ){
                damage += (2 * (int)DamageLevel) - 1;
	    }

	    return damage;
	}
	    
	[Constructable]
	public HeavyCrossbow() : base( 0x13FD )
	{
	    Weight = 9.0;
	    Layer = Layer.TwoHanded;
	}

	public HeavyCrossbow( Serial serial ) : base( serial )
	{
	}

	public override void Serialize( GenericWriter writer )
	{
	    base.Serialize( writer );

	    writer.Write( (int) 0 ); // version
	}

	public override void Deserialize( GenericReader reader )
	{
	    base.Deserialize( reader );

	    int version = reader.ReadInt();
	}
    }
}
