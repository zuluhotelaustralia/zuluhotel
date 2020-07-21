namespace Server.Items
{
    public class RightLeg : Item
	{

		[Constructible]
public RightLeg() : base( 0x1DA4 )
		{
		}

		[Constructible]
public RightLeg( Serial serial ) : base( serial )
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
