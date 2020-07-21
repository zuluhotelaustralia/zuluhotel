namespace Server.Items
{
    public class SummonDaemonScroll : SpellScroll
	{

		[Constructible]
public SummonDaemonScroll() : this( 1 )
		{
		}


		[Constructible]
public SummonDaemonScroll( int amount ) : base( 60, 0x1F69, amount )
		{
		}

		[Constructible]
public SummonDaemonScroll( Serial serial ) : base( serial )
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
