namespace Server.Items
{
    [FlipableAttribute( 0xDF1, 0xDF0 )]
	public class BlackStaff : BaseStaff
	{
		public override int DefaultStrengthReq{ get{ return 35; } }
		public override int DefaultMinDamage{ get{ return 8; } }
		public override int DefaultMaxDamage{ get{ return 33; } }
		public override int DefaultSpeed{ get{ return 35; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 70; } }


		[Constructible]
public BlackStaff() : base( 0xDF0 )
		{
			Weight = 6.0;
		}

		[Constructible]
public BlackStaff( Serial serial ) : base( serial )
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
