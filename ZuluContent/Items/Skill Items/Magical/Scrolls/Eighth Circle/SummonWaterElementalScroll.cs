namespace Server.Items
{
    public class SummonWaterElementalScroll : SpellScroll
	{

		[Constructible]
public SummonWaterElementalScroll() : this( 1 )
		{
		}


		[Constructible]
public SummonWaterElementalScroll( int amount ) : base( 63, 0x1F6C, amount )
		{
		}

		[Constructible]
public SummonWaterElementalScroll( Serial serial ) : base( serial )
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
