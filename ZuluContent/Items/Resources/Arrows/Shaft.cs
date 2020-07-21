namespace Server.Items
{
    public class Shaft : Item
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}


		[Constructible]
public Shaft() : this( 1 )
		{
		}


		[Constructible]
public Shaft( int amount ) : base( 0x1BD4 )
		{
			Stackable = true;
			Amount = amount;
		}

		[Constructible]
public Shaft( Serial serial ) : base( serial )
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
