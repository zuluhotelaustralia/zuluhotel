namespace Server.Items
{
    public class BladeSpiritsScroll : SpellScroll
	{

		[Constructible]
public BladeSpiritsScroll() : this( 1 )
		{
		}


		[Constructible]
public BladeSpiritsScroll( int amount ) : base( 32, 0x1F4D, amount )
		{
		}

		[Constructible]
public BladeSpiritsScroll( Serial serial ) : base( serial )
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
