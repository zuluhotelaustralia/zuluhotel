namespace Server.Items
{
	class WaterBarrel : BaseWaterContainer
	{
		public override int LabelNumber { get { return 1025453; } }  /* water barrel */

		public override int voidItem_ID { get { return vItemID; } }
		public override int fullItem_ID { get { return fItemID; } }
		public override int MaxQuantity { get { return 100; } }

		private static int vItemID = 0xe77;
		private static int fItemID = 0x154d;


		[Constructible]
public WaterBarrel()
			: this( false )
		{
		}


		[Constructible]
public WaterBarrel( bool filled )
			: base( filled ? fItemID : vItemID, filled )
		{
		}

		[Constructible]
public WaterBarrel( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
