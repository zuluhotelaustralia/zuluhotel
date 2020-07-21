namespace Server.Items
{
    public class RecallScroll : SpellScroll
	{

		[Constructible]
public RecallScroll() : this( 1 )
		{
		}


		[Constructible]
public RecallScroll( int amount ) : base( 31, 0x1F4C, amount )
		{
		}

		[Constructible]
public RecallScroll( Serial serial ) : base( serial )
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
