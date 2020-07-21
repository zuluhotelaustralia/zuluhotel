using System;

namespace Server.Items
{
    public class Brazier : BaseLight
	{
		public override int LitItemID{ get { return 0xE31; } }


		[Constructible]
public Brazier() : base( 0xE31 )
		{
			Movable = false;
			Duration = TimeSpan.Zero; // Never burnt out
			Burning = true;
			Light = LightType.Circle225;
			Weight = 20.0;
		}

		[Constructible]
public Brazier( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( IGenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( IGenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
