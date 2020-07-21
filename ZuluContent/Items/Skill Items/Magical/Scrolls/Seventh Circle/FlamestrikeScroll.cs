namespace Server.Items
{
    public class FlamestrikeScroll : SpellScroll
	{

		[Constructible]
public FlamestrikeScroll() : this( 1 )
		{
		}


		[Constructible]
public FlamestrikeScroll( int amount ) : base( 50, 0x1F5F, amount )
		{
		}

		[Constructible]
public FlamestrikeScroll( Serial serial ) : base( serial )
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
