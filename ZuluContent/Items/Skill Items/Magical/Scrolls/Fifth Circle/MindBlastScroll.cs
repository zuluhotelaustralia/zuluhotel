namespace Server.Items
{
    public class MindBlastScroll : SpellScroll
	{

		[Constructible]
public MindBlastScroll() : this( 1 )
		{
		}


		[Constructible]
public MindBlastScroll( int amount ) : base( 36, 0x1F51, amount )
		{
		}

		[Constructible]
public MindBlastScroll( Serial serial ) : base( serial )
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
