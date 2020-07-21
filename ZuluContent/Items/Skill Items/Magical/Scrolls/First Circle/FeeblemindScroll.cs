namespace Server.Items
{
    public class FeeblemindScroll : SpellScroll
	{

		[Constructible]
public FeeblemindScroll() : this( 1 )
		{
		}


		[Constructible]
public FeeblemindScroll( int amount ) : base( 2, 0x1F30, amount )
		{
		}

		[Constructible]
public FeeblemindScroll( Serial serial ) : base( serial )
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
