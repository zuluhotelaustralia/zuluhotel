namespace Server.Items
{
    public class GateTravelScroll : SpellScroll
	{

		[Constructible]
public GateTravelScroll() : this( 1 )
		{
		}


		[Constructible]
public GateTravelScroll( int amount ) : base( 51, 0x1F60, amount )
		{
		}

		[Constructible]
public GateTravelScroll( Serial serial ) : base( serial )
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
