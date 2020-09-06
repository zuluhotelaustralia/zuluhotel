namespace Server.Items
{
    [FlipableAttribute( 0x13B9, 0x13Ba )]
	public class VikingSword : BaseSword
	{
		public override int DefaultStrengthReq{ get{ return 40; } }
		public override int DefaultMinDamage{ get{ return 6; } }
		public override int DefaultMaxDamage{ get{ return 34; } }
		public override int DefaultSpeed{ get{ return 30; } }

		public override int DefaultHitSound{ get{ return 0x237; } }
		public override int DefaultMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 100; } }


		[Constructible]
public VikingSword() : base( 0x13B9 )
		{
			Weight = 6.0;
		}

		[Constructible]
public VikingSword( Serial serial ) : base( serial )
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
