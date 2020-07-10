namespace Server.Items
{
    [FlipableAttribute( 0x13b4, 0x13b3 )]
	public class Club : BaseBashing
	{
		public override int DefaultStrengthReq{ get{ return 10; } }
		public override int DefaultMinDamage{ get{ return 8; } }
		public override int DefaultMaxDamage{ get{ return 24; } }
		public override int DefaultSpeed{ get{ return 40; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 40; } }

		[Constructable]
		public Club() : base( 0x13B4 )
		{
			Weight = 9.0;
		}

		public Club( Serial serial ) : base( serial )
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