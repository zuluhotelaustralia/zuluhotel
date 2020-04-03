using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public abstract class BaseBashing : BaseMeleeWeapon
	{
		public override int DefHitSound{ get{ return 0x233; } }
		public override int DefMissSound{ get{ return 0x239; } }

		public override SkillName DefSkill{ get{ return SkillName.Macing; } }
		public override WeaponType DefType{ get{ return WeaponType.Bashing; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Bash1H; } }

		public BaseBashing( int itemID ) : base( itemID )
		{
		}

		public BaseBashing( Serial serial ) : base( serial )
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

		public override void OnHit( Mobile attacker, Mobile defender, double damageBonus )
		{
			base.OnHit( attacker, defender, damageBonus );

			int stamloss = Utility.Random( 1, 3 );

			if( attacker is PlayerMobile ){
			    PlayerMobile pm = attacker as PlayerMobile;
			    if( pm.Spec.SpecName == SpecName.Warrior ){
				stamloss *= 2;
			    }
			}
			
			defender.Stam -= stamloss;
		}

		public override double GetBaseDamage( Mobile attacker )
		{
			double damage = base.GetBaseDamage( attacker );

			/* no 
			if (!Core.AOS && (attacker.Player || attacker.Body.IsHuman) && Layer == Layer.TwoHanded && attacker.Skills[SkillName.Anatomy].Value >= 80 && (attacker.Skills[SkillName.Anatomy].Value / 400.0) >= Utility.RandomDouble() && Engines.ConPVP.DuelContext.AllowSpecialAbility(attacker, "Crushing Blow", false))
			{
				damage *= 1.5;

				attacker.SendMessage( "You deliver a crushing blow!" ); // Is this not localized?
				attacker.PlaySound( 0x11C );
			}*/

			return damage;
		}
	}
}
