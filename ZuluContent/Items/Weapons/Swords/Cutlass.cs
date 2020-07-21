namespace Server.Items
{
    [FlipableAttribute( 0x1441, 0x1440 )]
	public class Cutlass : BaseSword
	{
		public override int DefaultStrengthReq{ get{ return 10; } }
		public override int DefaultMinDamage{ get{ return 6; } }
		public override int DefaultMaxDamage{ get{ return 28; } }
		public override int DefaultSpeed{ get{ return 45; } }

		public override int DefHitSound{ get{ return 0x23B; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 70; } }


		[Constructible]
public Cutlass() : base( 0x1441 )
		{
			Weight = 8.0;
		}

		[Constructible]
public Cutlass( Serial serial ) : base( serial )
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
