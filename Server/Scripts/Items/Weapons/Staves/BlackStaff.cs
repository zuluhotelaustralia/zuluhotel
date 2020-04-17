using System;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
    [FlipableAttribute( 0xDF1, 0xDF0 )]
    public class BlackStaff : BaseStaff
    {
	public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.WhirlwindAttack; } }
	public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }

	public override int AosStrengthReq{ get{ return 35; } }
	public override int AosMinDamage{ get{ return 13; } }
	public override int AosMaxDamage{ get{ return 16; } }
	public override int AosSpeed{ get{ return 39; } }
	public override float MlSpeed{ get{ return 2.75f; } }

	public override int OldStrengthReq{ get{ return 35; } }
	public override int OldMinDamage{ get{ return 8; } }
	public override int OldMaxDamage{ get{ return 33; } }
	public override int OldSpeed{ get{ return 35; } }

	public override int InitMinHits{ get{ return 31; } }
	public override int InitMaxHits{ get{ return 70; } }

	public virtual double GetBaseDamage( Mobile attacker )
        {
	    int damage = Utility.Dice( 5, 6, 3 ); //t2a style yo

	    /* Apply damage level offset
             * : Regular : 0
             * : Ruin    : 1
             * : Might   : 3
             * : Force   : 5
             * : Power   : 7
             * : Vanq    : 9
             */
            if ( DamageLevel != WeaponDamageLevel.Regular )
                damage += (2 * (int)DamageLevel) - 1;

            return damage;
        }

	[Constructable]
	public BlackStaff() : base( 0xDF0 )
	{
	    Weight = 6.0;
	}

	public BlackStaff( Serial serial ) : base( serial )
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
