namespace Server.Items
{
    public class FarmableOnion : FarmableCrop
	{
		public static int GetCropID()
		{
			return 3183;
		}

		public override Item GetCropObject()
		{
			Onion onion = new Onion();

			onion.ItemID = Utility.Random( 3181, 2 );

			return onion;
		}

		public override int GetPickedID()
		{
			return 3254;
		}


		[Constructible]
public FarmableOnion() : base( GetCropID() )
		{
		}

		[Constructible]
public FarmableOnion( Serial serial ) : base( serial )
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
