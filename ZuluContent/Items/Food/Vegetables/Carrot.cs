namespace Server.Items
{
    [FlipableAttribute( 0xc77, 0xc78 )]
	public class Carrot : Food
	{

		[Constructible]
public Carrot() : this( 1 )
		{
		}


		[Constructible]
public Carrot( int amount ) : base( amount, 0xc78 )
		{
			Weight = 1.0;
			FillFactor = 1;
		}

		[Constructible]
public Carrot( Serial serial ) : base( serial )
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
