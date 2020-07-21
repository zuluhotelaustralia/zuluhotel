namespace Server.Items
{
    public class BlueBook : BaseBook
	{


		[Constructible]
public BlueBook() : base( 0xFF2, 40, true )
		{
		}


		[Constructible]
public BlueBook( int pageCount, bool writable ) : base( 0xFF2, pageCount, writable )
		{
		}


		[Constructible]
public BlueBook( string title, string author, int pageCount, bool writable ) : base( 0xFF2, title, author, pageCount, writable )
		{
		}

		// Intended for defined books only
		[Constructible]
public BlueBook( bool writable ) : base( 0xFF2, writable )
		{
		}

		[Constructible]
public BlueBook( Serial serial ) : base( serial )
		{
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}
	}
}
