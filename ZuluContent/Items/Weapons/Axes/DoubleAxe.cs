namespace Server.Items
{
    [FlipableAttribute( 0xf4b, 0xf4c )]
	public class DoubleAxe : BaseAxe
	{
		public override int DefaultStrengthReq{ get{ return 45; } }
		public override int DefaultMinDamage{ get{ return 5; } }
		public override int DefaultMaxDamage{ get{ return 35; } }
		public override int DefaultSpeed{ get{ return 37; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 110; } }


		[Constructible]
public DoubleAxe() : base( 0xF4B )
		{
			Weight = 8.0;
		}

		[Constructible]
public DoubleAxe( Serial serial ) : base( serial )
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
