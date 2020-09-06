namespace Server.Items
{
    [FlipableAttribute( 0x13FF, 0x13FE )]
	public class Katana : BaseSword
	{
		public override int DefaultStrengthReq{ get{ return 10; } }
		public override int DefaultMinDamage{ get{ return 5; } }
		public override int DefaultMaxDamage{ get{ return 26; } }
		public override int DefaultSpeed{ get{ return 58; } }

		public override int DefaultHitSound{ get{ return 0x23B; } }
		public override int DefaultMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 90; } }


		[Constructible]
public Katana() : base( 0x13FF )
		{
			Weight = 6.0;
		}

		[Constructible]
public Katana( Serial serial ) : base( serial )
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
