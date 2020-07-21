namespace Server.Items
{
    [FlipableAttribute( 0x170f, 0x1710 )]
	public class Shoes : BaseShoes
	{
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }


		[Constructible]
public Shoes() : this( 0 )
		{
		}


		[Constructible]
public Shoes( int hue ) : base( 0x170F, hue )
		{
			Weight = 2.0;
		}

		[Constructible]
public Shoes( Serial serial ) : base( serial )
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
