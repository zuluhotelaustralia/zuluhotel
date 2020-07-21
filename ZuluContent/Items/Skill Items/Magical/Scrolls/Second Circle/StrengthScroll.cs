namespace Server.Items
{
    public class StrengthScroll : SpellScroll
	{

		[Constructible]
public StrengthScroll() : this( 1 )
		{
		}


		[Constructible]
public StrengthScroll( int amount ) : base( 15, 0x1F3C, amount )
		{
		}

		[Constructible]
public StrengthScroll( Serial serial ) : base( serial )
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
