namespace Server.Items
{
    public class MagicUnTrapScroll : SpellScroll
	{

		[Constructible]
public MagicUnTrapScroll() : this( 1 )
		{
		}


		[Constructible]
public MagicUnTrapScroll( int amount ) : base( 13, 0x1F3A, amount )
		{
		}

		[Constructible]
public MagicUnTrapScroll( Serial serial ) : base( serial )
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
