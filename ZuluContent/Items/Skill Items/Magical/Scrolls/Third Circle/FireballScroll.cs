namespace Server.Items
{
    public class FireballScroll : SpellScroll
	{

		[Constructible]
public FireballScroll() : this( 1 )
		{
		}


		[Constructible]
public FireballScroll( int amount ) : base( 17, 0x1F3E, amount )
		{
		}

		[Constructible]
public FireballScroll( Serial serial ) : base( serial )
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
