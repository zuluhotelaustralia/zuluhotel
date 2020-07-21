namespace Server.Items
{
    public class GlacialStaff : BlackStaff
	{
		//TODO: Pre-AoS stuff
		public override int LabelNumber{ get{ return 1017413; } } // Glacial Staff


		[Constructible]
public GlacialStaff()
		{
			Hue = 0x480;
		}

		[Constructible]
public GlacialStaff( Serial serial ) : base( serial )
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
