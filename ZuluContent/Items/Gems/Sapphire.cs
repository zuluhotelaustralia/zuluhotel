namespace Server.Items
{
    public class Sapphire : Item
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}


		[Constructible]
public Sapphire() : this( 1 )
		{
		}


		[Constructible]
public Sapphire( int amount ) : base( 0xF19 )
		{
			Stackable = true;
			Amount = amount;
		}

		[Constructible]
public Sapphire( Serial serial ) : base( serial )
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
