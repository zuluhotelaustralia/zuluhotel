namespace Server.Items
{
    [FlipableAttribute( 0xF4D, 0xF4E )]
	public class Bardiche : BasePoleArm
	{
		public override int DefaultStrengthReq{ get{ return 40; } }
		public override int DefaultMinDamage{ get{ return 5; } }
		public override int DefaultMaxDamage{ get{ return 43; } }
		public override int DefaultSpeed{ get{ return 26; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 100; } }


		[Constructible]
public Bardiche() : base( 0xF4D )
		{
			Weight = 7.0;
		}

		[Constructible]
public Bardiche( Serial serial ) : base( serial )
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
