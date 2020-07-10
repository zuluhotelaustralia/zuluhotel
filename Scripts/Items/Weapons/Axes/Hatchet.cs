namespace Server.Items
{
    [FlipableAttribute( 0xF43, 0xF44 )]
	public class Hatchet : BaseAxe
	{
		public override int DefaultStrengthReq{ get{ return 15; } }
		public override int DefaultMinDamage{ get{ return 2; } }
		public override int DefaultMaxDamage{ get{ return 17; } }
		public override int DefaultSpeed{ get{ return 40; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 80; } }

		[Constructable]
		public Hatchet() : base( 0xF43 )
		{
			Weight = 4.0;
		}

		public Hatchet( Serial serial ) : base( serial )
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