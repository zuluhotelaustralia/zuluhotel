namespace Server.Items
{
    [Flipable( 0x1051, 0x1052 )]
	public class AxleGears : Item
	{

		[Constructible]
public AxleGears() : this( 1 )
		{
		}


		[Constructible]
public AxleGears( int amount ) : base( 0x1051 )
		{
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
		}

		[Constructible]
public AxleGears( Serial serial ) : base( serial )
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
