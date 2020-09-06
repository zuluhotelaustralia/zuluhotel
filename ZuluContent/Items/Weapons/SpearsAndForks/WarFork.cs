namespace Server.Items
{
    [FlipableAttribute( 0x1405, 0x1404 )]
	public class WarFork : BaseSpear
	{
		public override int DefaultStrengthReq{ get{ return 35; } }
		public override int DefaultMinDamage{ get{ return 4; } }
		public override int DefaultMaxDamage{ get{ return 32; } }
		public override int DefaultSpeed{ get{ return 45; } }

		public override int DefaultHitSound{ get{ return 0x236; } }
		public override int DefaultMissSound{ get{ return 0x238; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 110; } }

		public override WeaponAnimation DefaultAnimation{ get{ return WeaponAnimation.Pierce1H; } }


		[Constructible]
public WarFork() : base( 0x1405 )
		{
			Weight = 9.0;
		}

		[Constructible]
public WarFork( Serial serial ) : base( serial )
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
