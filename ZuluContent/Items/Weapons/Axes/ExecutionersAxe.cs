namespace Server.Items
{
    [FlipableAttribute( 0xf45, 0xf46 )]
	public class ExecutionersAxe : BaseAxe
	{
		public override int DefaultStrengthReq{ get{ return 35; } }
		public override int DefaultMinDamage{ get{ return 6; } }
		public override int DefaultMaxDamage{ get{ return 33; } }
		public override int DefaultSpeed{ get{ return 37; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 70; } }


		[Constructible]
public ExecutionersAxe() : base( 0xF45 )
		{
			Weight = 8.0;
		}

		[Constructible]
public ExecutionersAxe( Serial serial ) : base( serial )
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
