namespace Server.Items
{
    public class FarmableTurnip : FarmableCrop
	{
		public static int GetCropID()
		{
			return Utility.Random( 3169, 3 );
		}

		public override Item GetCropObject()
		{
			Turnip turnip = new Turnip();

			turnip.ItemID = Utility.Random( 3385, 2 );

			return turnip;
		}

		public override int GetPickedID()
		{
			return 3254;
		}


		[Constructible]
public FarmableTurnip() : base( GetCropID() )
		{
		}

		[Constructible]
public FarmableTurnip( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
