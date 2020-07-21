namespace Server.Items
{
    public class ArchProtectionScroll : SpellScroll
	{

		[Constructible]
public ArchProtectionScroll() : this( 1 )
		{
		}


		[Constructible]
public ArchProtectionScroll( int amount ) : base( 25, 0x1F46, amount )
		{
		}

		[Constructible]
public ArchProtectionScroll( Serial serial ) : base( serial )
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
