namespace Server.Items
{
    [FlipableAttribute( 0x13B8, 0x13B7 )]
	public class ThinLongsword : BaseSword
	{
		public override int DefaultStrengthReq{ get{ return 25; } }
		public override int DefaultMinDamage{ get{ return 5; } }
		public override int DefaultMaxDamage{ get{ return 33; } }
		public override int DefaultSpeed{ get{ return 35; } }

		public override int DefHitSound{ get{ return 0x237; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 110; } }


		[Constructible]
public ThinLongsword() : base( 0x13B8 )
		{
			Weight = 1.0;
		}

		[Constructible]
public ThinLongsword( Serial serial ) : base( serial )
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
