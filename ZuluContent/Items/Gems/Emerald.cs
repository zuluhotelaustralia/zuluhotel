namespace Server.Items
{
    public class Emerald : Item
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}


		[Constructible]
public Emerald() : this( 1 )
		{
		}


		[Constructible]
public Emerald( int amount ) : base( 0xF10 )
		{
			Stackable = true;
			Amount = amount;
		}

		[Constructible]
public Emerald( Serial serial ) : base( serial )
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
