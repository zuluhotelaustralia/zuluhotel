namespace Server.Items
{
	public class Wheat : Food
	{

		[Constructible]
public Wheat() : this( 1 )
		{
		}


		[Constructible]
public Wheat( int amount ) : base( amount, 0x1EBD)
		{
			Weight = 1.0;
			FillFactor = 5;
		}

		[Constructible]
public Wheat( Serial serial ) : base( serial )
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
