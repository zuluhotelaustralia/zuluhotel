namespace Server.Items
{
    public abstract class BaseMiddleTorso : BaseClothing
	{
		public BaseMiddleTorso( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseMiddleTorso( int itemID, int hue ) : base( itemID, Layer.MiddleTorso, hue )
		{
		}

		public BaseMiddleTorso( Serial serial ) : base( serial )
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
