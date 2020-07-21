namespace Server.Items
{
    public class FarmablePumpkin : FarmableCrop
	{
		public static int GetCropID()
		{
			return Utility.Random( 3166, 3 );
		}

		public override Item GetCropObject()
		{
			Pumpkin pumpkin = new Pumpkin();

			pumpkin.ItemID = Utility.Random( 3178, 3 );

			return pumpkin;
		}

		public override int GetPickedID()
		{
			return Utility.Random( 3166, 3 );
		}


		[Constructible]
public FarmablePumpkin()
			: base( GetCropID() )
		{
		}

		[Constructible]
public FarmablePumpkin( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
