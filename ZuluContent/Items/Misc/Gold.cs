namespace Server.Items
{
    public class Gold : Item
	{
		public override double DefaultWeight
		{
			get { return 0.02; }
		}


		[Constructible]
public Gold() : this( 1 )
		{
		}


		[Constructible]
public Gold( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}


		[Constructible]
public Gold( int amount ) : base( 0xEED )
		{
			Stackable = true;
			Amount = amount;
		}

		[Constructible]
public Gold( Serial serial ) : base( serial )
		{
		}

		public override int GetDropSound()
		{
			if ( Amount <= 1 )
				return 0x2E4;
			else if ( Amount <= 5 )
				return 0x2E5;
			else
				return 0x2E6;
		}

		protected override void OnAmountChange( int oldValue )
		{
			int newValue = Amount;

			UpdateTotal( this, TotalType.Gold, newValue - oldValue );
		}

		public override int GetTotal( TotalType type )
		{
			int baseTotal = base.GetTotal( type );

			if ( type == TotalType.Gold )
				baseTotal += Amount;

			return baseTotal;
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
