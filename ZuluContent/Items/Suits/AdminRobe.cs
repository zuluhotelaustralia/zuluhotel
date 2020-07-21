namespace Server.Items
{
    public class AdminRobe : BaseSuit
	{

		[Constructible]
public AdminRobe() : base( AccessLevel.Administrator, 0x0, 0x204F ) // Blank hue
		{
		}

		[Constructible]
public AdminRobe( Serial serial ) : base( serial )
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
