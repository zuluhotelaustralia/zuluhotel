namespace Server.Items
{
    public class DispelFieldScroll : SpellScroll
	{

		[Constructible]
public DispelFieldScroll() : this( 1 )
		{
		}


		[Constructible]
public DispelFieldScroll( int amount ) : base( 33, 0x1F4E, amount )
		{
		}

		[Constructible]
public DispelFieldScroll( Serial serial ) : base( serial )
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
