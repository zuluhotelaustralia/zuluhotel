namespace Server.Items
{
    public class PoisonScroll : SpellScroll
	{

		[Constructible]
public PoisonScroll() : this( 1 )
		{
		}


		[Constructible]
public PoisonScroll( int amount ) : base( 19, 0x1F40, amount )
		{
		}

		[Constructible]
public PoisonScroll( Serial serial ) : base( serial )
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
