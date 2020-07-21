using System;

namespace Server.Items
{
    public class BrazierTall : BaseLight
	{
		public override int LitItemID{ get { return 0x19AA; } }


		[Constructible]
public BrazierTall() : base( 0x19AA )
		{
			Movable = false;
			Duration = TimeSpan.Zero; // Never burnt out
			Burning = true;
			Light = LightType.Circle300;
			Weight = 25.0;
		}

		[Constructible]
public BrazierTall( Serial serial ) : base( serial )
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
