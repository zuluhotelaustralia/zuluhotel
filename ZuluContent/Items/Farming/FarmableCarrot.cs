namespace Server.Items
{
    public class FarmableCarrot : FarmableCrop
	{
		public static int GetCropID()
		{
			return 3190;
		}

		public override Item GetCropObject()
		{
			Carrot carrot = new Carrot();

			carrot.ItemID = Utility.Random( 3191, 2 );

			return carrot;
		}

		public override int GetPickedID()
		{
			return 3254;
		}


		[Constructible]
public FarmableCarrot() : base( GetCropID() )
		{
		}

		[Constructible]
public FarmableCarrot( Serial serial ) : base( serial )
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
