namespace Server.Items
{
    public class ParalyzeScroll : SpellScroll
	{

		[Constructible]
public ParalyzeScroll() : this( 1 )
		{
		}


		[Constructible]
public ParalyzeScroll( int amount ) : base( 37, 0x1F52, amount )
		{
		}

		[Constructible]
public ParalyzeScroll( Serial serial ) : base( serial )
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
