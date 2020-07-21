namespace Server.Items
{
    public class Beeswax : Item
	{

		[Constructible]
public Beeswax() : this( 1 )
		{
		}


		[Constructible]
public Beeswax( int amount ) : base( 0x1422 )
		{
			Weight = 1.0;
			Stackable = true;
			Amount = amount;
		}

		[Constructible]
public Beeswax( Serial serial ) : base( serial )
		{
		}



		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
