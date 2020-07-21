namespace Server.Items
{
    [Flipable( 0x1059, 0x105A )]
	public class SextantParts : Item
	{

		[Constructible]
public SextantParts() : this( 1 )
		{
		}


		[Constructible]
public SextantParts( int amount ) : base( 0x1059 )
		{
			Stackable = true;
			Amount = amount;
			Weight = 2.0;
		}

		[Constructible]
public SextantParts( Serial serial ) : base( serial )
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
