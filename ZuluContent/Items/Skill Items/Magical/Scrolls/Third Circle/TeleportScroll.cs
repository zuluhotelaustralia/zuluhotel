namespace Server.Items
{
    public class TeleportScroll : SpellScroll
	{

		[Constructible]
public TeleportScroll() : this( 1 )
		{
		}


		[Constructible]
public TeleportScroll( int amount ) : base( 21, 0x1F42, amount )
		{
		}

		[Constructible]
public TeleportScroll( Serial serial ) : base( serial )
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
