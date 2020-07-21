namespace Server.Items
{
    public class FruitBasket : Food
	{

		[Constructible]
public FruitBasket() : base( 1, 0x993 )
		{
			Weight = 2.0;
			FillFactor = 5;
			Stackable = false;
		}

		[Constructible]
public FruitBasket( Serial serial ) : base( serial )
		{
		}

		public override bool Eat( Mobile from )
		{
			if ( !base.Eat( from ) )
				return false;

			from.AddToBackpack( new Basket() );
			return true;
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
