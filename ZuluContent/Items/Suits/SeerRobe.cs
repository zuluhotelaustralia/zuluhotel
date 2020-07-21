namespace Server.Items
{
    public class SeerRobe : BaseSuit
	{

		[Constructible]
public SeerRobe() : base( AccessLevel.Seer, 0x1D3, 0x204F )
		{
		}

		[Constructible]
public SeerRobe( Serial serial ) : base( serial )
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
