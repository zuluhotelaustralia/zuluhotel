namespace Server.Items
{
    public class TelekinisisScroll : SpellScroll
	{

		[Constructible]
public TelekinisisScroll() : this( 1 )
		{
		}


		[Constructible]
public TelekinisisScroll( int amount ) : base( 20, 0x1F41, amount )
		{
		}

		[Constructible]
public TelekinisisScroll( Serial serial ) : base( serial )
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
