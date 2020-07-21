namespace Server.Items
{
    public class FireFieldScroll : SpellScroll
	{

		[Constructible]
public FireFieldScroll() : this( 1 )
		{
		}


		[Constructible]
public FireFieldScroll( int amount ) : base( 27, 0x1F48, amount )
		{
		}

		[Constructible]
public FireFieldScroll( Serial serial ) : base( serial )
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
