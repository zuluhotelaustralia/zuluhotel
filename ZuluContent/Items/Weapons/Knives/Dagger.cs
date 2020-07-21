namespace Server.Items
{
    [FlipableAttribute( 0xF52, 0xF51 )]
	public class Dagger : BaseKnife
	{
		public override int DefaultStrengthReq{ get{ return 1; } }
		public override int DefaultMinDamage{ get{ return 3; } }
		public override int DefaultMaxDamage{ get{ return 15; } }
		public override int DefaultSpeed{ get{ return 55; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 40; } }

		public override SkillName DefSkill{ get{ return SkillName.Fencing; } }
		public override WeaponType DefType{ get{ return WeaponType.Piercing; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Pierce1H; } }


		[Constructible]
public Dagger() : base( 0xF52 )
		{
			Weight = 1.0;
		}

		[Constructible]
public Dagger( Serial serial ) : base( serial )
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
		}
	}
}
