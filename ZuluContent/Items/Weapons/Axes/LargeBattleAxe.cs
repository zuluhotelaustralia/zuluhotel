namespace Server.Items
{
    [FlipableAttribute( 0x13FB, 0x13FA )]
	public class LargeBattleAxe : BaseAxe
	{
		public override int DefaultStrengthReq{ get{ return 40; } }
		public override int DefaultMinDamage{ get{ return 6; } }
		public override int DefaultMaxDamage{ get{ return 38; } }
		public override int DefaultSpeed{ get{ return 30; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 70; } }


		[Constructible]
public LargeBattleAxe() : base( 0x13FB )
		{
			Weight = 6.0;
		}

		[Constructible]
public LargeBattleAxe( Serial serial ) : base( serial )
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
