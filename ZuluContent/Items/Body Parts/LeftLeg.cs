namespace Server.Items
{
    public class LeftLeg : Item
	{

		[Constructible]
public LeftLeg() : base( 0x1DA3 )
		{
		}

		[Constructible]
public LeftLeg( Serial serial ) : base( serial )
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
