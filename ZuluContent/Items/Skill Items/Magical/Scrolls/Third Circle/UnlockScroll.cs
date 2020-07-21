namespace Server.Items
{
    public class UnlockScroll : SpellScroll
	{

		[Constructible]
public UnlockScroll() : this( 1 )
		{
		}


		[Constructible]
public UnlockScroll( int amount ) : base( 22, 0x1F43, amount )
		{
		}

		[Constructible]
public UnlockScroll( Serial serial ) : base( serial )
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
