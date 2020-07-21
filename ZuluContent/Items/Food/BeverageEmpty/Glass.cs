namespace Server.Items
{
    [FlipableAttribute( 0x1f81, 0x1f82, 0x1f83, 0x1f84 )]
	public class Glass : Item
	{

		[Constructible]
public Glass() : base( 0x1f81 )
		{
			Weight = 0.1;
		}

		[Constructible]
public Glass( Serial serial ) : base( serial )
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
