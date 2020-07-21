namespace Server.Items
{
    [FlipableAttribute( 0x13F6, 0x13F7 )]
	public class ButcherKnife : BaseKnife
	{
		public override int DefaultStrengthReq{ get{ return 5; } }
		public override int DefaultMinDamage{ get{ return 2; } }
		public override int DefaultMaxDamage{ get{ return 14; } }
		public override int DefaultSpeed{ get{ return 40; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 40; } }


		[Constructible]
public ButcherKnife() : base( 0x13F6 )
		{
			Weight = 1.0;
		}

		[Constructible]
public ButcherKnife( Serial serial ) : base( serial )
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
