namespace Server.Items
{
    [FlipableAttribute( 0x143D, 0x143C )]
	public class HammerPick : BaseBashing
	{
		public override int DefaultStrengthReq{ get{ return 35; } }
		public override int DefaultMinDamage{ get{ return 6; } }
		public override int DefaultMaxDamage{ get{ return 33; } }
		public override int DefaultSpeed{ get{ return 30; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 70; } }


		[Constructible]
public HammerPick() : base( 0x143D )
		{
			Weight = 9.0;
			Layer = Layer.OneHanded;
		}

		[Constructible]
public HammerPick( Serial serial ) : base( serial )
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
