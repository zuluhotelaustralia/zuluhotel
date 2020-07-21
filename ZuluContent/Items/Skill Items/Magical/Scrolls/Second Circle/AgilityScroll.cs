namespace Server.Items
{
    public class AgilityScroll : SpellScroll
	{

		[Constructible]
public AgilityScroll() : this( 1 )
		{
		}


		[Constructible]
public AgilityScroll( int amount ) : base( 8, 0x1F35, amount )
		{
		}

		[Constructible]
public AgilityScroll( Serial serial ) : base( serial )
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
