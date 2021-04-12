namespace Server.Items
{
    public class Globe : BaseTinkerItem
	{

		[Constructible]
public Globe() : base( 0x1047 ) // It isn't flipable
		{
			Weight = 3.0;
		}

		[Constructible]
public Globe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize(IGenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
