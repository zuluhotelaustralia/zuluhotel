using System;

namespace Server.Items
{
    public class CandelabraStand : BaseLight
	{
		public override int LitItemID{ get { return 0xB26; } }
		public override int UnlitItemID{ get { return 0xA29; } }


		[Constructible]
public CandelabraStand() : base( 0xA29 )
		{
			Duration = TimeSpan.Zero; // Never burnt out
			Burning = false;
			Light = LightType.Circle225;
			Weight = 20.0;
		}

		[Constructible]
public CandelabraStand( Serial serial ) : base( serial )
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
