namespace Server.Items
{
    public class WallOfStoneScroll : SpellScroll
	{

		[Constructible]
public WallOfStoneScroll() : this( 1 )
		{
		}


		[Constructible]
public WallOfStoneScroll( int amount ) : base( 23, 0x1F44, amount )
		{
		}

		[Constructible]
public WallOfStoneScroll( Serial serial ) : base( serial )
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
