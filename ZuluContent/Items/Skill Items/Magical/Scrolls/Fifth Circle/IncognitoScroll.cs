namespace Server.Items
{
    public class IncognitoScroll : SpellScroll
	{

		[Constructible]
public IncognitoScroll() : this( 1 )
		{
		}


		[Constructible]
public IncognitoScroll( int amount ) : base( 34, 0x1F4F, amount )
		{
		}

		[Constructible]
public IncognitoScroll( Serial serial ) : base( serial )
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
