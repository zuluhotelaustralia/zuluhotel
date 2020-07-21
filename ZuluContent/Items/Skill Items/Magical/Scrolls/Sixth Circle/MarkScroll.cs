namespace Server.Items
{
    public class MarkScroll : SpellScroll
	{

		[Constructible]
public MarkScroll() : this( 1 )
		{
		}


		[Constructible]
public MarkScroll( int amount ) : base( 44, 0x1F59, amount )
		{
		}

		[Constructible]
public MarkScroll( Serial serial ) : base( serial )
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
