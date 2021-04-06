namespace Server.Items
{
    [Flipable( 0x1053, 0x1054 )]
	public class Gears : BaseTinkerItem
	{

		[Constructible]
public Gears() : this( 1 )
		{
		}


		[Constructible]
public Gears( int amount ) : base( 0x1053 )
		{
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
		}

		[Constructible]
public Gears( Serial serial ) : base( serial )
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
