namespace Server.Items
{
    public class BlazeDyeTub : DyeTub
	{

		[Constructible]
public BlazeDyeTub()
		{
			Hue = DyedHue = 0x489;
			Redyable = false;
		}

		[Constructible]
public BlazeDyeTub( Serial serial ) : base( serial )
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
