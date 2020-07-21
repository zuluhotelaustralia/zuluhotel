namespace Server.Items
{
    public class SummonFireElementalScroll : SpellScroll
	{

		[Constructible]
public SummonFireElementalScroll() : this( 1 )
		{
		}


		[Constructible]
public SummonFireElementalScroll( int amount ) : base( 62, 0x1F6B, amount )
		{
		}

		[Constructible]
public SummonFireElementalScroll( Serial serial ) : base( serial )
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
