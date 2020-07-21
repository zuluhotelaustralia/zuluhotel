namespace Server.Items
{
    public class RevealScroll : SpellScroll
	{

		[Constructible]
public RevealScroll() : this( 1 )
		{
		}


		[Constructible]
public RevealScroll( int amount ) : base( 47, 0x1F5C, amount )
		{
		}

		[Constructible]
public RevealScroll( Serial serial ) : base( serial )
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
