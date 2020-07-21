namespace Server.Items
{
    [Flipable( 0x104F, 0x1050 )]
	public class ClockParts : Item
	{

		[Constructible]
public ClockParts() : this( 1 )
		{
		}


		[Constructible]
public ClockParts( int amount ) : base( 0x104F )
		{
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
		}

		[Constructible]
public ClockParts( Serial serial ) : base( serial )
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
