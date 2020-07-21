namespace Server.Items
{
    [FlipableAttribute( 0xFAF, 0xFB0 )]
	[Engines.Craft.Anvil]
	public class Anvil : Item
	{

		[Constructible]
public Anvil() : base( 0xFAF )
		{
			Movable = false;
		}

		[Constructible]
public Anvil( Serial serial ) : base( serial )
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

	[Engines.Craft.Forge]
	public class Forge : Item
	{

		public Forge() : base( 0xFB1 )
		{
			Movable = false;
		}

		public Forge( Serial serial ) : base( serial )
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
