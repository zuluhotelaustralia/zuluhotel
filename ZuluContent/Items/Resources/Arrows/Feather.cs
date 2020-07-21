namespace Server.Items
{
    public class Feather : Item
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}


		[Constructible]
public Feather() : this( 1 )
		{
		}


		[Constructible]
public Feather( int amount ) : base( 0x1BD1 )
		{
			Stackable = true;
			Amount = amount;
		}

		[Constructible]
public Feather( Serial serial ) : base( serial )
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
