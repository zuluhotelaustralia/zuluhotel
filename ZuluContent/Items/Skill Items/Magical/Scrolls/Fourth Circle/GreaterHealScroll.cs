namespace Server.Items
{
    public class GreaterHealScroll : SpellScroll
	{

		[Constructible]
public GreaterHealScroll() : this( 1 )
		{
		}


		[Constructible]
public GreaterHealScroll( int amount ) : base( 28, 0x1F49, amount )
		{
		}

		[Constructible]
public GreaterHealScroll( Serial serial ) : base( serial )
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
