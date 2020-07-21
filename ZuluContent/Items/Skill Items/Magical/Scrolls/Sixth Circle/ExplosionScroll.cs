namespace Server.Items
{
    public class ExplosionScroll : SpellScroll
	{

		[Constructible]
public ExplosionScroll() : this( 1 )
		{
		}


		[Constructible]
public ExplosionScroll( int amount ) : base( 42, 0x1F57, amount )
		{
		}

		[Constructible]
public ExplosionScroll( Serial serial ) : base( serial )
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
