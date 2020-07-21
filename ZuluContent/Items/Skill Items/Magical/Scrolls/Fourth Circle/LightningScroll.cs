namespace Server.Items
{
    public class LightningScroll : SpellScroll
	{

		[Constructible]
public LightningScroll() : this( 1 )
		{
		}


		[Constructible]
public LightningScroll( int amount ) : base( 29, 0x1F4A, amount )
		{
		}

		[Constructible]
public LightningScroll( Serial serial ) : base( serial )
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
