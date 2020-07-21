namespace Server.Items
{
    public abstract class BasePants : BaseClothing
	{
		public BasePants( int itemID ) : this( itemID, 0 )
		{
		}

		public BasePants( int itemID, int hue ) : base( itemID, Layer.Pants, hue )
		{
		}

		public BasePants( Serial serial ) : base( serial )
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
