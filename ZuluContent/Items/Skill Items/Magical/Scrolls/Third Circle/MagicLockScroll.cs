namespace Server.Items
{
    public class MagicLockScroll : SpellScroll
	{

		[Constructible]
public MagicLockScroll() : this( 1 )
		{
		}


		[Constructible]
public MagicLockScroll( int amount ) : base( 18, 0x1F3F, amount )
		{
		}

		[Constructible]
public MagicLockScroll( Serial serial ) : base( serial )
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
