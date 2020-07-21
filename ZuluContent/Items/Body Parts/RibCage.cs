namespace Server.Items
{
    [FlipableAttribute( 0x1B17, 0x1B18 )]
	public class RibCage : Item, IScissorable
	{

		[Constructible]
public RibCage() : base( 0x1B17 + Utility.Random( 2 ) )
		{
			Stackable = false;
			Weight = 5.0;
		}

		[Constructible]
public RibCage( Serial serial ) : base( serial )
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

		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) )
				return false;

			base.ScissorHelper( from, new Bone(), Utility.RandomMinMax( 3, 5 ) );

			return true;
		}
	}
}
