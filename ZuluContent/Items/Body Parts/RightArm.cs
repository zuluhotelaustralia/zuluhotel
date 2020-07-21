namespace Server.Items
{
    public class RightArm : Item
	{

		[Constructible]
public RightArm() : base( 0x1DA2 )
		{
		}

		[Constructible]
public RightArm( Serial serial ) : base( serial )
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
