namespace Server.Items
{
    [FlipableAttribute( 0xEC4, 0xEC5 )]
	public class SkinningKnife : BaseKnife
	{
		public override int DefaultStrengthReq{ get{ return 5; } }
		public override int DefaultMinDamage{ get{ return 1; } }
		public override int DefaultMaxDamage{ get{ return 10; } }
		public override int DefaultSpeed{ get{ return 40; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 40; } }

		[Constructable]
		public SkinningKnife() : base( 0xEC4 )
		{
			Weight = 1.0;
		}

		public SkinningKnife( Serial serial ) : base( serial )
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