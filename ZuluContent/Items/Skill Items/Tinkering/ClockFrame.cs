namespace Server.Items
{
    [Flipable( 0x104D, 0x104E )]
	public class ClockFrame : Item
	{

		[Constructible]
public ClockFrame() : this( 1 )
		{
		}


		[Constructible]
public ClockFrame( int amount ) : base( 0x104D )
		{
			Stackable = true;
			Amount = amount;
			Weight = 2.0;
		}

		[Constructible]
public ClockFrame( Serial serial ) : base( serial )
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
