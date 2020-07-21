namespace Server.Items
{
    public class BlankMap : MapItem
	{

		[Constructible]
public BlankMap()
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			SendLocalizedMessageTo( from, 500208 ); // It appears to be blank.
		}

		[Constructible]
public BlankMap( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
