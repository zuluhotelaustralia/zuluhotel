namespace Server.Items
{
    [FlipableAttribute( 0xF5C, 0xF5D )]
	public class Mace : BaseBashing
	{
		public override int DefaultStrengthReq{ get{ return 20; } }
		public override int DefaultMinDamage{ get{ return 8; } }
		public override int DefaultMaxDamage{ get{ return 32; } }
		public override int DefaultSpeed{ get{ return 30; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 70; } }


		[Constructible]
public Mace() : base( 0xF5C )
		{
			Weight = 14.0;
		}

		[Constructible]
public Mace( Serial serial ) : base( serial )
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
