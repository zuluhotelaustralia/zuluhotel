namespace Server.Items
{
    public class EnergyFieldScroll : SpellScroll
	{

		[Constructible]
public EnergyFieldScroll() : this( 1 )
		{
		}


		[Constructible]
public EnergyFieldScroll( int amount ) : base( 49, 0x1F5E, amount )
		{
		}

		[Constructible]
public EnergyFieldScroll( Serial serial ) : base( serial )
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
