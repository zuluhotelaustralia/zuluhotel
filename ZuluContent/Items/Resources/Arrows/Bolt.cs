namespace Server.Items
{
    public class Bolt : Item
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}


		[Constructible]
public Bolt() : this( 1 )
		{
		}


		[Constructible]
public Bolt( int amount ) : base( 0x1BFB )
		{
			Stackable = true;
			Amount = amount;
		}

		[Constructible]
public Bolt( Serial serial ) : base( serial )
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
