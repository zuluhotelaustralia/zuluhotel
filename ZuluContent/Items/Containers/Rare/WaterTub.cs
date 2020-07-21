namespace Server.Items
{
	class Tub : BaseWaterContainer
	{
		public override int voidItem_ID { get { return vItemID; } }
		public override int fullItem_ID { get { return fItemID; } }
		public override int MaxQuantity { get { return 50; } }

		private static int vItemID = 0xe83;
		private static int fItemID = 0xe7b;


		[Constructible]
public Tub()
			: this( false )
		{
		}


		[Constructible]
public Tub( bool filled )
			: base( filled ? fItemID : vItemID, filled )
		{
		}

		[Constructible]
public Tub( Serial serial )
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
