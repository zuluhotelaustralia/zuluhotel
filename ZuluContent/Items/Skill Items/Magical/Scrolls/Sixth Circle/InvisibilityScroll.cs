namespace Server.Items
{
    public class InvisibilityScroll : SpellScroll
	{

		[Constructible]
public InvisibilityScroll() : this( 1 )
		{
		}


		[Constructible]
public InvisibilityScroll( int amount ) : base( 43, 0x1F58, amount )
		{
		}

		[Constructible]
public InvisibilityScroll( Serial serial ) : base( serial )
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
