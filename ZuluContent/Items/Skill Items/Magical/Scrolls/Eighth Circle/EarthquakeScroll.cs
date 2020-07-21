namespace Server.Items
{
    public class EarthquakeScroll : SpellScroll
	{

		[Constructible]
public EarthquakeScroll() : this( 1 )
		{
		}


		[Constructible]
public EarthquakeScroll( int amount ) : base( 56, 0x1F65, amount )
		{
		}

		[Constructible]
public EarthquakeScroll( Serial serial ) : base( serial )
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
