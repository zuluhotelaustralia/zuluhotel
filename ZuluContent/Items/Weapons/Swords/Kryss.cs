namespace Server.Items
{
    [FlipableAttribute( 0x1401, 0x1400 )]
	public class Kryss : BaseSword
	{
		public override int DefaultStrengthReq{ get{ return 10; } }
		public override int DefaultMinDamage{ get{ return 3; } }
		public override int DefaultMaxDamage{ get{ return 28; } }
		public override int DefaultSpeed{ get{ return 53; } }

		public override int DefaultHitSound{ get{ return 0x23C; } }
		public override int DefaultMissSound{ get{ return 0x238; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 90; } }

		public override SkillName DefaultSkill{ get{ return SkillName.Fencing; } }
		public override WeaponType DefaultWeaponType{ get{ return WeaponType.Piercing; } }
		public override WeaponAnimation DefaultAnimation{ get{ return WeaponAnimation.Pierce1H; } }


		[Constructible]
public Kryss() : base( 0x1401 )
		{
			Weight = 2.0;
		}

		[Constructible]
public Kryss( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Weight == 1.0 )
				Weight = 2.0;
		}
	}
}
