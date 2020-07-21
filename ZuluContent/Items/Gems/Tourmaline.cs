namespace Server.Items
{
    public class Tourmaline : Item
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}


		[Constructible]
public Tourmaline() : this( 1 )
		{
		}


		[Constructible]
public Tourmaline( int amount ) : base( 0xF2D )
		{
			Stackable = true;
			Amount = amount;
		}

		[Constructible]
public Tourmaline( Serial serial ) : base( serial )
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
