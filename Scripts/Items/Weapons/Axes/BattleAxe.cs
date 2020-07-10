namespace Server.Items
{
    [FlipableAttribute( 0xF47, 0xF48 )]
	public class BattleAxe : BaseAxe
	{
		public override int DefaultStrengthReq{ get{ return 40; } }
		public override int DefaultMinDamage{ get{ return 6; } }
		public override int DefaultMaxDamage{ get{ return 38; } }
		public override int DefaultSpeed{ get{ return 30; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 70; } }

		[Constructable]
		public BattleAxe() : base( 0xF47 )
		{
			Weight = 4.0;
			Layer = Layer.TwoHanded;
		}

		public BattleAxe( Serial serial ) : base( serial )
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