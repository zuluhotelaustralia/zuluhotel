namespace Server.Items
{
    [FlipableAttribute( 0x1439, 0x1438 )]
	public class WarHammer : BaseBashing
	{
		public override int DefaultStrengthReq{ get{ return 40; } }
		public override int DefaultMinDamage{ get{ return 8; } }
		public override int DefaultMaxDamage{ get{ return 36; } }
		public override int DefaultSpeed{ get{ return 31; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 110; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Bash2H; } }


		[Constructible]
public WarHammer() : base( 0x1439 )
		{
			Weight = 10.0;
			Layer = Layer.TwoHanded;
		}

		[Constructible]
public WarHammer( Serial serial ) : base( serial )
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
