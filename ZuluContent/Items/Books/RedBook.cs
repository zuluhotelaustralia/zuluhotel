namespace Server.Items
{
    public class RedBook : BaseBook
	{

		[Constructible]
public RedBook() : base( 0xFF1 )
		{
		}


		[Constructible]
public RedBook( int pageCount, bool writable ) : base( 0xFF1, pageCount, writable )
		{
		}


		[Constructible]
public RedBook( string title, string author, int pageCount, bool writable ) : base( 0xFF1, title, author, pageCount, writable )
		{
		}

		// Intended for defined books only
		[Constructible]
public RedBook( bool writable ) : base( 0xFF1, writable )
		{
		}

		[Constructible]
public RedBook( Serial serial ) : base( serial )
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
