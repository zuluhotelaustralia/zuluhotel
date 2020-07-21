namespace Server.Items
{
    public class ManaVampireScroll : SpellScroll
	{

		[Constructible]
public ManaVampireScroll() : this( 1 )
		{
		}


		[Constructible]
public ManaVampireScroll( int amount ) : base( 52, 0x1F61, amount )
		{
		}

		[Constructible]
public ManaVampireScroll( Serial serial ) : base( serial )
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
