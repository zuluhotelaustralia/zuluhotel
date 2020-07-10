namespace Server.Items
{
    [FlipableAttribute( 0x143B, 0x143A )]
	public class Maul : BaseBashing
	{
		public override int DefaultStrengthReq{ get{ return 20; } }
		public override int DefaultMinDamage{ get{ return 10; } }
		public override int DefaultMaxDamage{ get{ return 30; } }
		public override int DefaultSpeed{ get{ return 30; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 70; } }

		[Constructable]
		public Maul() : base( 0x143B )
		{
			Weight = 10.0;
		}

		public Maul( Serial serial ) : base( serial )
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

			if ( Weight == 14.0 )
				Weight = 10.0;
		}
	}
}