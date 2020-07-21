namespace Server.Items
{
    [Flipable( 0x105B, 0x105C )]
	public class Axle : Item
	{

		[Constructible]
public Axle() : this( 1 )
		{
		}


		[Constructible]
public Axle( int amount ) : base( 0x105B )
		{
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
		}

		[Constructible]
public Axle( Serial serial ) : base( serial )
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
