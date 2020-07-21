namespace Server.Items
{
    public class EnergyVortexScroll : SpellScroll
	{

		[Constructible]
public EnergyVortexScroll() : this( 1 )
		{
		}


		[Constructible]
public EnergyVortexScroll( int amount ) : base( 57, 0x1F66, amount )
		{
		}

		[Constructible]
public EnergyVortexScroll( Serial serial ) : base( serial )
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
