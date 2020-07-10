namespace Server.Items
{
    public class MagicWand : BaseBashing
	{
		public override int DefaultStrengthReq{ get{ return 0; } }
		public override int DefaultMinDamage{ get{ return 2; } }
		public override int DefaultMaxDamage{ get{ return 6; } }
		public override int DefaultSpeed{ get{ return 35; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 110; } }

		[Constructable]
		public MagicWand() : base( 0xDF2 )
		{
			Weight = 1.0;
		}

		public MagicWand( Serial serial ) : base( serial )
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