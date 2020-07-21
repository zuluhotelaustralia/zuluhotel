namespace Server.Items
{
    public class FarmableWheat : FarmableCrop
	{
		public static int GetCropID()
		{
			return Utility.Random( 3157, 4 );
		}

		public override Item GetCropObject()
		{
			return new WheatSheaf();
		}

		public override int GetPickedID()
		{
			return Utility.Random( 3502, 2 );
		}


		[Constructible]
public FarmableWheat() : base( GetCropID() )
		{
		}

		[Constructible]
public FarmableWheat( Serial serial ) : base( serial )
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