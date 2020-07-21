namespace Server.Items
{
    [FlipableAttribute( 0x13B6, 0x13B5 )]
	public class Scimitar : BaseSword
	{
		public override int DefaultStrengthReq{ get{ return 10; } }
		public override int DefaultMinDamage{ get{ return 4; } }
		public override int DefaultMaxDamage{ get{ return 30; } }
		public override int DefaultSpeed{ get{ return 43; } }

		public override int DefHitSound{ get{ return 0x23B; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 90; } }


		[Constructible]
public Scimitar() : base( 0x13B6 )
		{
			Weight = 5.0;
		}

		[Constructible]
public Scimitar( Serial serial ) : base( serial )
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
