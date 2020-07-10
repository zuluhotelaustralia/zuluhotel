namespace Server.Items
{
    [FlipableAttribute( 0x1443, 0x1442 )]
	public class TwoHandedAxe : BaseAxe
	{
		public override int DefaultStrengthReq{ get{ return 35; } }
		public override int DefaultMinDamage{ get{ return 5; } }
		public override int DefaultMaxDamage{ get{ return 39; } }
		public override int DefaultSpeed{ get{ return 30; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 90; } }

		[Constructable]
		public TwoHandedAxe() : base( 0x1443 )
		{
			Weight = 8.0;
		}

		public TwoHandedAxe( Serial serial ) : base( serial )
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
	}
}